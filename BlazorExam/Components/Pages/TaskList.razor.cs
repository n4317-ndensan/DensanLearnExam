using Microsoft.AspNetCore.Components;
using BlazorExam.Services;
using BlazorExam.Models;

namespace BlazorExam.Components.Pages
{
    public partial class TaskList
    {
        [Inject]
        protected NavigationManager? NavigationManager { get; set; }

        [Inject]
        protected IStateBox<List<TaskModel>> TaskSession { get; set; } = default!;

        private List<TaskModel> tasks = new List<TaskModel>();

        private bool hasSession => TaskSession.HasState;

        protected override async Task OnInitializedAsync()
        {
            if (hasSession)
            {
                tasks = TaskSession.State!;
                tasks.Sort((x, y) => x.CompareTo(y));
            }
            else
            {
                await GetFirstData();
            }
        }

        private void HandleAddTask()
        {
            NavigationManager?.NavigateTo("/addtask");
        }

        private async Task GetFirstData()
        {
            // 3���̃^�X�N��ǉ�
            tasks.Add(new TaskModel
            {
                Title = "�^�X�N1",
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskModel.TaskModelStatus.������,
                Content = "�^�X�N1�̓��e�ł��B"
            });

            tasks.Add(new TaskModel
            {
                Title = "�^�X�N2",
                DueDate = DateTime.Now.AddDays(-2),
                Status = TaskModel.TaskModelStatus.�d�|��,
                Content = "�^�X�N2�̓��e�ł��B\n���e��2�s�ڂł��B"
            });

            tasks.Add(new TaskModel
            {
                Title = "�^�X�N3",
                DueDate = DateTime.Now.AddDays(3),
                Status = TaskModel.TaskModelStatus.����,
                Content = "�^�X�N3�̓��e�ł��B"
            });

            await Task.Delay(200);

            if (!hasSession)
            {
                TaskSession.State = tasks;
            }
        }
    }
}