using Horizon.Areas.Purchases.ViewModel.PurchaseOrderVMs;
using Horizon.Models.Shared;

namespace Horizon.Areas.Purchases.Validations
{
    public static class PurchaseOrderValidator
    {
        public static void Validate(PurchaseOrderVM vm)
        {
            if (string.IsNullOrWhiteSpace(vm.PurchaseOrderDate))
                throw new Exception("برجاء إدخال تاريخ أمر الإنتاج");

            if (string.IsNullOrWhiteSpace(vm.DeliveryDate))
                throw new Exception("برجاء إدخال تاريخ التوريد");

            if (vm.SupplierId <= 0)
                throw new Exception("برجاء اختيار المورد");

            var hasProducts = vm.PurchaseOrderDetails.Any(d => d.RecordStatus != RecordStatus.Deleted);
            var hasRawItems = vm.PurchaseOrderItemRawDetails.Any(d => d.RecordStatus != RecordStatus.Deleted);

            if (!hasProducts && !hasRawItems)
                throw new Exception("يجب إدخال منتج واحد على الأقل أو عنصر خام");

            //if (vm.TotalAmount <= 0)
            //    throw new Exception("قيمة أمر الإنتاج يجب أن تكون أكبر من 0");

            foreach (var item in vm.PurchaseOrderDetails.Where(d => d.RecordStatus != RecordStatus.Deleted))
            {
                ValidateDetail(item, "منتج");
            }

            foreach (var item in vm.PurchaseOrderItemRawDetails.Where(d => d.RecordStatus != RecordStatus.Deleted))
            {
                ValidateDetail(item, "عنصر خام");
            }
        }

        private static void ValidateDetail(PurchaseOrderDetailsVM detail, string type)
        {
            if (detail.StoreItemId <= 0)
                throw new Exception($"برجاء اختيار {type}");

            if (detail.StoreItemAmount <= 0)
                throw new Exception($"برجاء إدخال الكمية لـ {type}");
        }
    }

}
