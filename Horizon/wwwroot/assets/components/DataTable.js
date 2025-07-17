// wwwroot/js/components/DataTable.js
export const DataTable = {
    props: {
        columns: {
            type: Array,
            required: true
        },
        data: {
            type: Array,
            required: true
        },
        searchable: {
            type: Boolean,
            default: false
        },
        sortable: {
            type: Boolean,
            default: false
        },
        paginated: {
            type: Boolean,
            default: false
        },
        perPageOptions: {
            type: Array,
            default: () => [10, 25, 50, 100]
        },
        defaultPerPage: {
            type: Number,
            default: 10
        },
        searchPlaceholder: {
            type: String,
            default: 'البحث...'
        },
        noDataMessage: {
            type: String,
            default: 'لا توجد بيانات'
        },
        tableClass: {
            type: String,
            default: 'w-full'
        },
        headerClass: {
            type: String,
            default: 'bg-gray-50'
        },
        rowClass: {
            type: String,
            default: 'hover:bg-gray-50 transition-colors'
        }
    },
    emits: ['row-action', 'row-click', 'sort-change', 'search-change', 'page-change'],
    data() {
        return {
            searchQuery: '',
            currentPage: 1,
            perPage: this.defaultPerPage,
            sortColumn: null,
            sortDirection: 'asc'
        }
    },
    computed: {
        filteredData() {
            if (!this.searchable || !this.searchQuery) {
                return this.data;
            }
            
            const filtered = this.data.filter(item => {
                return this.columns.some(column => {
                    if (column.searchable === false) return false;
                    const value = this.getNestedValue(item, column.key);
                    return value && value.toString().toLowerCase().includes(this.searchQuery.toLowerCase());
                });
            });
            
            this.$emit('search-change', {
                query: this.searchQuery,
                results: filtered.length
            });
            
            return filtered;
        },
        
        sortedData() {
            if (!this.sortable || !this.sortColumn) {
                return this.filteredData;
            }
            
            const sorted = [...this.filteredData].sort((a, b) => {
                const aValue = this.getNestedValue(a, this.sortColumn);
                const bValue = this.getNestedValue(b, this.sortColumn);
                
                // Handle null/undefined values
                if (aValue == null && bValue == null) return 0;
                if (aValue == null) return 1;
                if (bValue == null) return -1;
                
                // Handle numbers
                if (!isNaN(aValue) && !isNaN(bValue)) {
                    return this.sortDirection === 'asc' ? aValue - bValue : bValue - aValue;
                }
                
                // Handle strings
                const aStr = aValue.toString().toLowerCase();
                const bStr = bValue.toString().toLowerCase();
                
                if (aStr < bStr) {
                    return this.sortDirection === 'asc' ? -1 : 1;
                }
                if (aStr > bStr) {
                    return this.sortDirection === 'asc' ? 1 : -1;
                }
                return 0;
            });
            
            this.$emit('sort-change', {
                column: this.sortColumn,
                direction: this.sortDirection
            });
            
            return sorted;
        },
        
        paginatedData() {
            if (!this.paginated) {
                return this.sortedData;
            }
            
            const start = (this.currentPage - 1) * this.perPage;
            const end = start + this.perPage;
            return this.sortedData.slice(start, end);
        },
        
        totalPages() {
            return Math.ceil(this.sortedData.length / this.perPage);
        },
        
        paginationInfo() {
            const start = (this.currentPage - 1) * this.perPage + 1;
            const end = Math.min(start + this.perPage - 1, this.sortedData.length);
            return {
                start,
                end,
                total: this.sortedData.length
            };
        },
        
        visiblePages() {
            const current = this.currentPage;
            const total = this.totalPages;
            const delta = 2;
            
            let start = Math.max(1, current - delta);
            let end = Math.min(total, current + delta);
            
            if (end - start < 2 * delta) {
                start = Math.max(1, end - 2 * delta);
                end = Math.min(total, start + 2 * delta);
            }
            
            return Array.from({ length: end - start + 1 }, (_, i) => start + i);
        }
    },
    watch: {
        searchQuery() {
            this.currentPage = 1;
        },
        data() {
            this.currentPage = 1;
        }
    },
    methods: {
        getNestedValue(obj, path) {
            return path.split('.').reduce((o, p) => o && o[p], obj);
        },
        
        sort(column) {
            if (!this.sortable || !column.sortable) return;
            
            if (this.sortColumn === column.key) {
                // Cycle through: asc -> desc -> null (cancel sort)
                if (this.sortDirection === 'asc') {
                    this.sortDirection = 'desc';
                } else if (this.sortDirection === 'desc') {
                    // Cancel sort - reset to no sorting
                    this.sortColumn = null;
                    this.sortDirection = 'asc';
                }
            } else {
                // New column clicked, start with ascending
                this.sortColumn = column.key;
                this.sortDirection = 'asc';
            }
        },
        
        getSortIcon(column) {
            if (!column.sortable || this.sortColumn !== column.key) {
                return 'fa-sort text-gray-400';
            }
            return this.sortDirection === 'asc' ? 'fa-sort-up text-blue-600' : 'fa-sort-down text-blue-600';
        },
        
        goToPage(page) {
            if (page >= 1 && page <= this.totalPages) {
                this.currentPage = page;
                this.$emit('page-change', {
                    page: page,
                    perPage: this.perPage
                });
            }
        },
        
        changePerPage() {
            this.currentPage = 1;
            this.$emit('page-change', {
                page: 1,
                perPage: this.perPage
            });
        },
        
        handleAction(action, item) {
            this.$emit('row-action', { action, item });
        },
        
        handleRowClick(item) {
            this.$emit('row-click', item);
        },
        
        renderCell(item, column) {
            if (column.render) {
                return column.render(item);
            }
            return this.getNestedValue(item, column.key);
        },
        
        // Public methods for external control
        resetSearch() {
            this.searchQuery = '';
        },
        
        resetSort() {
            this.sortColumn = null;
            this.sortDirection = 'asc';
        },
        
        resetPagination() {
            this.currentPage = 1;
        },
        
        exportData() {
            return {
                filtered: this.filteredData,
                sorted: this.sortedData,
                paginated: this.paginatedData
            };
        }
    },
    template: `
        <section class="bg-gray-50 cancel-dark:bg-gray-900">
            <div class="mx-auto">
                <!-- Start coding here -->
                <div class="bg-white cancel-dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">
                    <div class="flex flex-col md:flex-row items-center justify-between space-y-3 md:space-y-0 md:space-x-4 p-4">
                        <div class="w-full md:w-1/2">
                            <form class="flex items-center"  v-if="searchable">
                                <label for="simple-search" class="sr-only">Search</label>
                                <div class="relative w-full">
                                    <div class="absolute inset-y-0 left-0 flex items-center pr-3 pointer-events-none">
                                        <svg aria-hidden="true" class="w-5 h-5 text-gray-500 cancel-dark:text-gray-400"
                                            fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                            <path fill-rule="evenodd"
                                                d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                                                clip-rule="evenodd" />
                                        </svg>
                                    </div>
                                    <input type="text" id="simple-search"
                                        class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg  focus:border-gray-500 block w-full pr-10 p-2 cancel-dark:bg-gray-700 cancel-dark:border-gray-600 cancel-dark:placeholder-:ring-gray-500 cancel-dark:focus:border-gray-500 shadow-none max-w-[300px]"
                                        v-model="searchQuery"
                                        style="box-shadow: none"
                                        :placeholder="searchPlaceholder"
                                        required="">
                                    <button
                                        v-if="searchQuery"
                                        @click="resetSearch"
                                        class="absolute left-3 top-3 text-gray-400 hover:text-gray-600"
                                    >
                                        <i class="fa fa-times"></i>
                                    </button>
                                </div>
                            </form>
                        </div>
                        <div
                            class="flex flex-col md:flex-row space-y-2 md:space-y-0 items-stretch md:items-center justify-end md:space-x-3 flex-shrink-0">
                            <div class="flex items-center space-x-3">
                                <div class="flex items-center gap-2" v-if="paginated">
                                    <label class="text-sm text-gray-600">عرض:</label>
                                    <select 
                                        v-model="perPage" 
                                        @change="changePerPage"
                                        class="bg-gray-50 border border-gray-300 rounded px-3 pl-7 text-right py-1 text-sm  focus:border-gray-500 outline-none focus:shadow-none" style="box-shadow: none"
                                    >
                                        <option v-for="option in perPageOptions" :key="option" :value="option">
                                            {{ option }}
                                        </option>
                                    </select>
                                    <span class="text-sm text-gray-600">من {{ sortedData.length }}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="overflow-x-auto">
                        <table class="w-full text-sm text-right text-gray-500 cancel-dark:text-gray-400">
                            <thead class="text-xs text-gray-700 uppercase bg-gray-50 cancel-dark:bg-gray-700 cancel-dark:text-gray-400">
                                <tr>
                                    <th 
                                        v-for="column in columns" 
                                        :key="column.key"
                                        :class="[
                                            'px-6 py-3 text-right font-bold text-xs text-gray-500 uppercase tracking-wider whitespace-nowrap',
                                            column.sortable && sortable ? 'cursor-pointer hover:bg-gray-100' : '',
                                            column.width || '',
                                            column.headerClass || ''
                                        ]"
                                        @click="sort(column)"
                                    >
                                        <div class="flex items-center justify-between">
                                            <span>{{ column.label }}</span>
                                            <i 
                                                v-if="column.sortable && sortable"
                                                :class="['fa', getSortIcon(column)]"
                                            ></i>
                                        </div>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr 
                                    v-for="(item, index) in paginatedData" 
                                    :key="item.id || index"
                                    class="border-b"
                                    @click="handleRowClick(item)"
                                >
                                    <td 
                                        v-for="column in columns" 
                                        :key="column.key"
                                        :class="[
                                            'px-4 py-3',
                                            column.cellClass || ''
                                        ]"
                                    >
                                        <div v-if="column.type === 'actions'" class="flex gap-2">
                                            <button
                                                v-for="action in column.actions"
                                                :key="action.key"
                                                @click.stop="handleAction(action.key, item)"
                                                :class="[
                                                    'px-3 py-1 text-sm rounded-md transition-colors',
                                                    action.class || 'bg-blue-600 hover:bg-blue-700 text-white'
                                                ]"
                                                :disabled="action.disabled ? action.disabled(item) : false"
                                            >
                                                {{ action.label }}
                                            </button>
                                        </div>
                                        <div v-else-if="column.type === 'link'">
                                            <a 
                                                :href="column.href ? column.href(item) : '#'"
                                                :class="column.linkClass || 'text-blue-600 hover:text-blue-800'"
                                                @click.stop
                                            >
                                                {{ renderCell(item, column) }}
                                            </a>
                                        </div>
                                        <div v-else-if="column.type === 'badge'">
                                            <span :class="[
                                                'inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium',
                                                column.badgeClass ? column.badgeClass(item) : 'bg-gray-100 text-gray-800'
                                            ]">
                                                {{ renderCell(item, column) }}
                                            </span>
                                        </div>
                                        <div v-else-if="column.type === 'html'" v-html="renderCell(item, column)"></div>
                                        <div v-else class="text-sm text-gray-900">
                                            {{ renderCell(item, column) || "-" }}
                                        </div>
                                    </td>
                                </tr>
                                <tr v-if="paginatedData.length === 0">
                                    <td :colspan="columns.length" class="px-6 py-8 text-center text-gray-500">
                                        {{ noDataMessage }}
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                    <nav v-if="paginated && totalPages > 1" class="flex flex-col md:flex-row justify-between items-start md:items-center space-y-3 md:space-y-0 p-4"
                        aria-label="Table navigation">
                        <span class="text-sm font-normal text-gray-500 cancel-dark:text-gray-400">
                            عرض
                            <span class="font-semibold text-gray-900 cancel-dark:text-white">{{ paginationInfo.start }} إلى {{ paginationInfo.end }}</span>
                            من
                            <span class="font-semibold text-gray-900 cancel-dark:text-white">{{ paginationInfo.total }} نتيجة</span>
                        </span>
                        <ul class="inline-flex items-stretch -space-x-px">
                            <li>
                                <buton
                                    @click="goToPage(currentPage - 1)"
                                    :disabled="currentPage === 1"
                                    class="flex items-center justify-center h-full py-1.5 px-3 leading-tight text-gray-500 bg-white rounded-r-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 cancel-dark:bg-gray-800 cancel-dark:border-gray-700 cancel-dark:text-gray-400 cancel-dark:hover:bg-gray-700 cancel-dark:hover:text-white">
                                    <span class="sr-only">Next</span>
                                    <svg class="w-5 h-5" aria-hidden="true" fill="currentColor" viewbox="0 0 20 20"
                                        xmlns="http://www.w3.org/2000/svg">
                                        <path fill-rule="evenodd"
                                            d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"
                                            clip-rule="evenodd" />
                                    </svg>
                                </buton>
                            </li>
                            <li v-for="page in visiblePages" :key="page">
                                <button @click="goToPage(page)"
                                    :class="['flex items-center justify-center text-sm py-2 px-3 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 cancel-dark:bg-gray-800 cancel-dark:border-gray-700 cancel-dark:text-gray-400 cancel-dark:hover:bg-gray-700 cancel-dark:hover:text-white', currentPage === page ? 'font-bold' : '']">{{ page }}</button>
                            </li>
                            <li>
                                <button
                                    @click="goToPage(currentPage + 1)"
                                    :disabled="currentPage === totalPages"
                                    class="flex items-center justify-center h-full py-1.5 px-3 ml-0 text-gray-500 bg-white rounded-l-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 cancel-dark:bg-gray-800 cancel-dark:border-gray-700 cancel-dark:text-gray-400 cancel-dark:hover:bg-gray-700 cancel-dark:hover:text-white">
                                    <span class="sr-only">Previous</span>
                                    <svg class="w-5 h-5" aria-hidden="true" fill="currentColor" viewbox="0 0 20 20"
                                        xmlns="http://www.w3.org/2000/svg">
                                        <path fill-rule="evenodd"
                                            d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z"
                                            clip-rule="evenodd" />
                                    </svg>
                                </button>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </section>
    `
};