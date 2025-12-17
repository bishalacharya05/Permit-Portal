using Microsoft.AspNetCore.Mvc;
using permit_portal.Models.DomainModel;
using permit_portal.Models.ViewModel;
using permit_portal.Data;
using Microsoft.EntityFrameworkCore;
namespace permit_portal.Controllers
{
    public class TouristPermitController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;
        public TouristPermitController(ApplicationDbContext context, IEmailService emailService)
        {
            this.emailService = emailService;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(TouristPermitViewModel touristPermitViewModel)
        {
            if (ModelState.IsValid)
            {
                //Mapping the domain model TouristPermit details to the view model TouristPermitViewModel details
                var touristPermit = new TouristPermit
                {
                    FullName = touristPermitViewModel.FullName,
                    Email = touristPermitViewModel.Email,
                    Nationality = touristPermitViewModel.Nationality,
                    ContactNumber = touristPermitViewModel.ContactNumber,
                    PermitLocation = touristPermitViewModel.PermitLocation,
                    VisitDate = touristPermitViewModel.VisitDate,
                    NumberOfPeople = touristPermitViewModel.NumberOfPeople,
                    Purpose = touristPermitViewModel.Purpose
                };

                context.TouristPermits.Add(touristPermit);
                context.SaveChanges();


            }

            // Send confirmation email
            try
            {
                string subject = "Application Confirmation";
                string body = $"Hello {touristPermitViewModel.FullName},\n\n" +
                              $"Your Application is  for {touristPermitViewModel.VisitDate:dd-MM-yyyy} at.\n" +
                              $"Purpose: {touristPermitViewModel.Purpose}\n\nThank you for using our service!";

                await emailService.SendEmail(touristPermitViewModel.Email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending failed: " + ex.Message);
                ViewBag.EmailError = "Application sent but failed to send email.";
            }
            return RedirectToAction("Apply");
        }

        public IActionResult Details()
        {
            //Using dbcontext to read the TouristsPermits
            var touristPermits = context.TouristPermits.ToList();
            //passint the touristPermits inside the view() because we can access the data of the TouristPermit table from the database
            return View(touristPermits);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //In this get part we get the id of the applicant  and store it to the variable
            var touristPermit = context.TouristPermits.Find(id);

            if (touristPermit == null)
            {
                return NotFound();
            }
            // Mapping the data to the domain model to the view model so we can pass the data in the input field of the edit form
            var viewModel = new TouristPermitViewModel
            {
                FullName = touristPermit.FullName,
                Email = touristPermit.Email,
                Nationality = touristPermit.Nationality,
                ContactNumber = touristPermit.ContactNumber,
                PermitLocation = touristPermit.PermitLocation,
                VisitDate = touristPermit.VisitDate,
                NumberOfPeople = touristPermit.NumberOfPeople,
                Purpose = touristPermit.Purpose
            };
            // Passing the viewModel data to the view of the Edit View
            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Edit(TouristPermitViewModel viewmodel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }
            var touristPermit = context.TouristPermits.Find(viewmodel.Id);

            if (touristPermit == null)
            {
                return NotFound();
            }

            touristPermit.FullName = viewmodel.FullName;
            touristPermit.Email = viewmodel.Email;
            touristPermit.Nationality = viewmodel.Nationality;
            touristPermit.ContactNumber = viewmodel.ContactNumber;
            touristPermit.PermitLocation = viewmodel.PermitLocation;
            touristPermit.VisitDate = viewmodel.VisitDate;
            touristPermit.NumberOfPeople = viewmodel.NumberOfPeople;
            touristPermit.Purpose = viewmodel.Purpose;


            context.TouristPermits.Update(touristPermit);
            context.SaveChanges();
            return RedirectToAction("Details");


        }
        

        [HttpPost]
        public IActionResult Delete (int id)
        {
            var touristPermit = context.TouristPermits.Find(id);

            if(touristPermit!=null)
            {
                context.TouristPermits.Remove(touristPermit);
                context.SaveChanges();
                return RedirectToAction("Details");

            }

            return RedirectToAction("Details");

            
        }

    }

       
    
}
