using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using test1apdb.Models;

namespace test1apdb.Services
{
    public class TaskService : ITaskService
    {
        public List<MyTask> GetTasks(int IdProject)
        {
            bool IsIdEmpty = IdProject == 0;
            List<MyTask> tasks = new List<MyTask>();
            using (var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
            using (var com = new SqlCommand())

            {

                con.Open();
                com.Connection = con;
                if (IsIdEmpty)
                {
                    com.CommandText = " Select t.IdTask as Task, t.Name as NameTask, t.Description as DescriptionTask, t.Deadline as DeadlineTask, p.Name as NameProject, r.Name as NameTaskType, m.LastName as NameAssign, n.LastName as NameCreator " +
                        "from Task t join Project p on p.IdProject = t.IdProject " +
                        "join TaskType r on t.IdTaskType = r.IdTaskType " +
                        "join TeamMember m on m.IdTeamMember = t.IdAssignedTo " +
                        "join TeamMember n on n.IdTeamMember= t.IdCreator " +
                        "order by t.Deadline desc;";
                }
                else
                {
                    com.CommandText = " Select t.IdTask as Task, t.Name as NameTask, t.Description as DescriptionTask, t.Deadline as DeadlineTask, p.Name as NameProject, r.Name as NameTaskType, m.LastName as NameAssign, n.LastName as NameCreator " +
                        "from Task t join Project p on p.IdProject = t.IdProject " +
                        "join TaskType r on t.IdTaskType = r.IdTaskType " +
                        "join TeamMember m on m.IdTeamMember = t.IdAssignedTo " +
                        "join TeamMember n on n.IdTeamMember= t.IdCreator " +
                        "where t.IdProject = @param " +
                        "order by t.Deadline desc;";
                    com.Parameters.AddWithValue("@param", IdProject);
                }
                var response = com.ExecuteReader();
                while (response.Read())
                {
                    tasks.Add(new MyTask
                    {
                        IdTask = Int32.Parse(response["Task"].ToString()),
                        Name = response["NameTask"].ToString(),
                        Description = response["DescriptionTask"].ToString(),
                        Deadline = response["DeadlineTask"].ToString(),
                        ProjectName = response["NameProject"].ToString(),
                        TaskTypeName = response["NameTaskType"].ToString(),
                        AssignedName = response["NameAssign"].ToString(),
                        CreatorName = response["NameCreator"].ToString()
                    });
                    
                }
                response.Close();


            }
            return tasks;
        }
        public int AddTask(MyTask task)
        {
            using (var con = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s18692;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                con.Open();
                com.Connection = con;
                var transaction = con.BeginTransaction();
                try
                {
                    int Id;
                    com.CommandText = "select max(IdTask)+1 from Task;";
                    var IdReader = com.ExecuteReader();
                    if (IdReader.Read())
                        Id = Int32.Parse(IdReader["max(IdTask)+1"].ToString());
                    else
                    {
                        transaction.Rollback();
                        return -1;
                    }
                    IdReader.Close();

                    com.CommandText = "insert into Task(IdTask,Name,Description,Deadline,IdProject,IdTaskType,IdAssignedTo,IdCreator) " +
                        "Values(@IdTask,@Name,@Description,@Deadline,@IdProject,@IdTaskType,@IdAssignedTo,@IdCreator);";
                    com.Parameters.AddWithValue("@IdTask", Id);
                    com.Parameters.AddWithValue("@Name", task.Name);
                    com.Parameters.AddWithValue("@Description", task.Description);
                    com.Parameters.AddWithValue("@Deadline", task.Deadline);
                    com.Parameters.AddWithValue("@IdProject", task.IdProject);
                    com.Parameters.AddWithValue("@IdTaskType", task.IdTaskType);
                    com.Parameters.AddWithValue("@IdAssignedTo", task.IdAssignedTo);
                    com.Parameters.AddWithValue("@IdCreator", task.IdCreator);
                    int v = com.ExecuteNonQuery();
                    transaction.Commit();
                    return Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return -1;
                }

            }
                
        }
    }
}
