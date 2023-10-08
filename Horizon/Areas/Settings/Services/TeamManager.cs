namespace Horizon.Areas.Settings.Services
{
    public class TeamManager //: ISPAManagerWithViewModel<Team, TeamViewModel>
    {
        //private readonly IGenericRepo<Team> _Repo;
        //private readonly IMapper _Mapper;
        //private readonly ApplicationDbContext _db;
        //public TeamManager(IGenericRepo<Team> Repo, ApplicationDbContext db, IMapper Mapper)
        //{
        //    _Repo = Repo;
        //    _Mapper = Mapper;
        //    _db = db;
        //}
        //public IEnumerable<TeamViewModel> GetList()
        //{
        //    var list = _Mapper.Map<List<TeamViewModel>>(_Repo.All());
        //    list.ForEach(x =>
        //    {
        //        x.SalesReps = _db.ApplicationUsers.Where(s => s.TeamId == x.Id).Select(x => x.Name).ToList();
        //    });
        //    return list;
        //}
        //public SPAViewModel<TeamViewModel> NewVW()
        //{
        //    return new SPAViewModel<TeamViewModel>()
        //    {

        //        TypesList = GetList(),
        //        Entity = new(),
        //        Messages = new List<string>(),
        //        SaveURL = string.Empty
        //    };
        //}
        //public FeedBackWithMessages SaveEntity(TeamViewModel vm)
        //{
        //    var result = new FeedBackWithMessages();
        //    try
        //    {
        //        var model = _Mapper.Map<Team>(vm);
        //        _Repo.Update(model);
        //        result.Messages.Add("Added Successfuly");
        //        result.Done = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Done = false;
        //        result.Messages.AddRange(ex.GetExpctionErrors());
        //    }

        //    return result;
        //}

    }
}
