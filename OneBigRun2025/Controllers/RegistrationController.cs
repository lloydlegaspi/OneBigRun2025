using Microsoft.AspNetCore.Mvc;
using OneBigRun2025.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace OneBigRun2025.Controllers
{
    public class RegistrationController : Controller
    {

        // GET: Registration
        public IActionResult Index()
        {
            return View(new UserModel());
        }

        // POST: Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel participant)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-GJBOKJCL;Initial Catalog=OneBigRun2025;Integrated Security=True;Trust Server Certificate=True"))
                {
                    conn.Open();

                    // Insert participant data into Participants table
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Participants] (Name, Age, ShirtSize, Email, PhoneNumber) OUTPUT INSERTED.ParticipantID VALUES (@Name, @Age, @ShirtSize, @Email, @PhoneNumber)", conn);
                    cmd.Parameters.AddWithValue("@Name", participant.Name);
                    cmd.Parameters.AddWithValue("@Age", participant.Age);
                    cmd.Parameters.AddWithValue("@ShirtSize", participant.ShirtSize);
                    cmd.Parameters.AddWithValue("@Email", participant.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", participant.PhoneNumber);

                    // Get the newly inserted ParticipantID
                    int participantId = (int)cmd.ExecuteScalar();

                    // Insert registration data into Registration table
                    SqlCommand regCmd = new SqlCommand("INSERT INTO [Registration] (ParticipantID, Category, RegistrationDate) VALUES (@ParticipantID, @Category, @RegistrationDate)", conn);
                    regCmd.Parameters.AddWithValue("@ParticipantID", participantId);
                    regCmd.Parameters.AddWithValue("@Category", participant.Category);
                    regCmd.Parameters.AddWithValue("@RegistrationDate", participant.RegistrationDate);

                    regCmd.ExecuteNonQuery();

                    conn.Close();
                }

                return RedirectToAction("Confirmation", participant);
            }
            return View("Index", participant);
        }

        // GET: Confirmation
        public IActionResult Confirmation(UserModel participant)
        {
            return View(participant);
        }
    }
}
