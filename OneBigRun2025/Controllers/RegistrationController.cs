using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBigRun2025.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace OneBigRun2025.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly AppDbContext _dbContext;

        public RegistrationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Registration
        public IActionResult Index()
        {
            return View(new UserModel());
        }

        // POST: Registration using ADO.NET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterWithAdo(UserModel participant)
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

                    int participantId = (int)cmd.ExecuteScalar();

                    // Insert registration data into Registration table
                    SqlCommand regCmd = new SqlCommand("INSERT INTO [Registration] (ParticipantID, Category, RegistrationDate) VALUES (@ParticipantID, @Category, @RegistrationDate)", conn);
                    regCmd.Parameters.AddWithValue("@ParticipantID", participantId);
                    regCmd.Parameters.AddWithValue("@Category", participant.Category);
                    regCmd.Parameters.AddWithValue("@RegistrationDate", participant.RegistrationDate);

                    regCmd.ExecuteNonQuery();
                }

                return RedirectToAction("Confirmation", participant);
            }
            return View("Index", participant);
        }

        // POST: Registration using Entity Framework (ORM)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterWithOrm(UserModel participant)
        {
            if (ModelState.IsValid)
            {
                // Add participant to the Participants table
                var newParticipant = new Participant
                {
                    Name = participant.Name,
                    Age = participant.Age,
                    ShirtSize = participant.ShirtSize,
                    Email = participant.Email,
                    PhoneNumber = participant.PhoneNumber,
                };
                _dbContext.Participants.Add(newParticipant);
                _dbContext.SaveChanges();

                // Add registration to the Registration table
                var newRegistration = new Registration
                {
                    ParticipantID = newParticipant.ParticipantID,
                    Category = participant.Category,
                    RegistrationDate = DateTime.Now
                };
                _dbContext.Registration.Add(newRegistration);
                _dbContext.SaveChanges();

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

