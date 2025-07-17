using Microsoft.AspNetCore.Components;
using Models;
using Repositories;

namespace FejlRapporter.Components.Pages;

public partial class Home
{
    public ErrorReport ErrorReportModel { get; set; } = new();
    [Inject] public ErrorReportRepo ErrorReportRepo { get; set; }

    private string message;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            StateHasChanged();
        }
    }


    private async Task HandleSubmit()
    {

        var creationSuccess = await ErrorReportRepo.CreateErrorReportAsync(ErrorReportModel);
        //kald repo
        if (creationSuccess)
        {

            message = "Fejlen er indrapporteret. Tak!";
            ErrorReportModel = new(); // nulstil form
        }
        else
        {
            message = "Noget gik galt. Prøv igen.";
        }
    }
}

