using System.ComponentModel.DataAnnotations;

namespace BlazorExam.Models;

public class TaskModel
{
        public Guid ID { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "タイトルは必須です。")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "期限は必須です。")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "状態は必須です。")]
        public TaskModelStatus Status { get; set; } = TaskModelStatus.未着手;

        public string Content { get; set; } = string.Empty;

        public enum TaskModelStatus
        {
            未着手 = 0,
            仕掛中 = 1,
            完了 = 2,
            無視 = 9
        }

        public int CompareTo(TaskModel other)
        {
            // Compare statuses in ascending order
            int statusComparison = this.Status.CompareTo(other.Status);

            // If statuses are equal, sort by DueDate
            if (statusComparison == 0)
            {
                // Determine if DueDate is greater than DateTime.Now
                bool thisIsFuture = this.DueDate > DateTime.Now;
                bool otherIsFuture = other.DueDate > DateTime.Now;

                // If 'this' is a future task and 'other' is not 
                // Then 'this' comes before 'other'
                if (thisIsFuture && !otherIsFuture)
                {
                    return -1;
                }
                // If 'other' is a future task and 'this' is not
                // Then 'other' comes before 'this'
                else if (!thisIsFuture && otherIsFuture)
                {
                    return 1;
                }
                // If both are future tasks or both are overdue
                // Then Sort by DueDate in ascending order (earliest first)
                else
                {
                    return this.DueDate.CompareTo(other.DueDate);
                }
            }

            // Return the result of status comparison if they are not equal
            return statusComparison;
        }
}