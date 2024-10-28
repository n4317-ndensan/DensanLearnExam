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
            // 3件のタスクを追加
            tasks.Add(new TaskModel
            {
                Title = "タスク1",
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskModel.TaskModelStatus.未着手,
                Content = "タスク1の内容です。"
            });

            tasks.Add(new TaskModel
            {
                Title = "タスク2",
                DueDate = DateTime.Now.AddDays(-2),
                Status = TaskModel.TaskModelStatus.仕掛中,
                Content = "タスク2の内容です。\n内容の2行目です。"
            });

            tasks.Add(new TaskModel
            {
                Title = "タスク3",
                DueDate = DateTime.Now.AddDays(3),
                Status = TaskModel.TaskModelStatus.完了,
                Content = "タスク3の内容です。"
            });

            await Task.Delay(200);

            if (!hasSession)
            {
                TaskSession.State = tasks;
            }
        }
    }
}