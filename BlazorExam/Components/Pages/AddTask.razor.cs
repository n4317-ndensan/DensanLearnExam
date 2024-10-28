using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorExam.Services;
using BlazorExam.Models;
using Radzen;
using Radzen.Blazor;

namespace BlazorExam.Components.Pages
{
    public partial class AddTask
    {
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        [Inject]
        protected IStateBox<List<TaskModel>> TaskSession { get; set; } = default!;

        private TaskModel taskModel = new TaskModel() { DueDate = DateTime.Now};

        private void HandleValidSubmit()
        {
            if (TaskSession.HasState)
            {
                TaskSession.State!.Add(taskModel);
            }

            NavigationManager?.NavigateTo("/tasklist");
        }
        private void HandleCancel()
        {
            NavigationManager?.NavigateTo("/tasklist");
        }
    }
}