using DatabaseService;
using Models;

namespace Repositories
{
    public class ErrorReportRepo
    {
        private readonly DbService dbService;

        public ErrorReportRepo(DbService dbService) {
            this.dbService = dbService;
        }


        public async Task<List<ErrorReport>> GetAllAsync()
        {
            return await dbService.GetAllAsync();
        }


        public async Task<bool> UpdateErrorReportStatus(ErrorReport errorReport)
        {
           return await dbService.UpdateErrorReportStatusAsync(errorReport);
        }

        public async Task<bool> CreateErrorReportAsync(ErrorReport errorReport)
        {
            //Repo minimum validering
            if (errorReport.Name is null) return false;
            if (errorReport.Email is null) return false;
            if (errorReport.Phonenumber is null) return false;
            if (errorReport.ErrorDescription is null) return false;


            return await dbService.CreateErrorReportAsync(errorReport);
        }
    }
}
