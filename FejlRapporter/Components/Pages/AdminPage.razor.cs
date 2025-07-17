using Microsoft.AspNetCore.Components;
using Models;
using Repositories;

namespace FejlRapporter.Components.Pages;

public partial class AdminPage
{

    public List<ErrorReport> ErrorReports { get; set; } //denne liste bilver vist på UI
    [Inject] public ErrorReportRepo ErrorReportRepo { get; set; }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            //hent alle fejl rapporter
            ErrorReports = await ErrorReportRepo.GetAllAsync();
            
            StateHasChanged();
        }

    }

    
    public async void UpdateStatus(ErrorReport errorReport, int status)
    {
        //hvis brugeren trykker på den nuværende status (knap) sæt status til deafault
        if (errorReport.Status == (ErrorStatus)status) status = 0;

        //sæt status
        errorReport.Status = (ErrorStatus)status;

        //kald repo
        await ErrorReportRepo.UpdateErrorReportStatus(errorReport);

        StateHasChanged();
    }

}
