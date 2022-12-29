
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public ActorsController(MoviesContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET Actors
        [HttpGet]
        public IActionResult Index()
        {
            var actors = _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(_context.Actors.ToList());
            return View(actors);
        }

        // Get Actors/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));
           
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        //Get Actors/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Post Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,BirthDate")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_mapper.Map<Actor>(inputModel));
                _context.SaveChanges();
                _logger.LogInformation($"Actor has been added!\nFirstName: {inputModel.FirstName}\nLastName: {inputModel.LastName}\nBirthdate: {inputModel.BirthDate.ToShortDateString()}");

                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);
        }

        [HttpGet]
        // Get: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<EditActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));
            _logger.LogInformation($"Actor has been edited!\nFirstName: {editModel.FirstName}\nLastName: {editModel.LastName}\nBirthdate: {editModel.BirthDate.ToShortDateString()}");

            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        // Post Actors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,BirthDate")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = _mapper.Map<Actor>(editModel);
                    actor.Id = id;
                    _context.Update(actor);
                    _context.SaveChanges();
                    _logger.LogInformation($"Actor has been updated!\nFirstName: {editModel.FirstName}\nLastName: {editModel.LastName}\nBirthdate: {editModel.BirthDate.ToShortDateString()}");

                }
                catch (DbUpdateException)
                {
                    if (!ActorExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
        }

        [HttpGet]

        // Get Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteActor = _mapper.Map<DeleteActorViewModel>(_context.Actors.FirstOrDefault(a => a.Id == id));
            if (deleteActor == null)
            {
                return NotFound();
            }

            return View(deleteActor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _context.Actors.Find(id);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            _logger.LogError($"Actor has been deleted!\nId: {actor.Id}\nFirstName: {actor.FirstName}\nLastName: {actor.LastName}\nBirthdate: {actor.BirthDate.ToShortDateString()}");
            return RedirectToAction(nameof(Index));
        }
        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}