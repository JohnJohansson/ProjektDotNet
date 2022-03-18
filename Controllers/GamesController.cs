#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektDotNet.Data;
using ProjektDotNet.Models;
using System.Drawing;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Authorization;

namespace ProjektDotNet.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GamesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: Games
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Games.Include(g => g.Condition).Include(g => g.GameConsole).Include(g => g.GameContent).Include(g => g.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Games for the public site
        [Route("/Spel")]
        public async Task<IActionResult> PublicGames()
        {
            var applicationDbContext = _context.Games.Include(g => g.Condition).Include(g => g.GameConsole).Include(g => g.GameContent).Include(g => g.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Games/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Condition)
                .Include(g => g.GameConsole)
                .Include(g => g.GameContent)
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ConditionFK"] = new SelectList(_context.Conditions, "Id", "Condtition");
            ViewData["GameConsoleFK"] = new SelectList(_context.GameConsoles, "Id", "ConsoleName");
            ViewData["GameContentFK"] = new SelectList(_context.GameContents, "Id", "Content");
            ViewData["PublisherFK"] = new SelectList(_context.Publishers, "Id", "PublisherName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ImgFile,PublisherFK,GameConsoleFK,ConditionFK,GameContentFK")] Game game)
        {

         //Uploding of images ---------------------------------------
            if (ModelState.IsValid)
            {
                //checks if the image is uploaded
                if(game.ImgFile != null)
                {
                    //uplode the image
                    string wwwRootPath = _webHostEnvironment.WebRootPath; //the path to the wwwroot folder where the images gets saved
                    string fileName = Path.GetFileNameWithoutExtension(game.ImgFile.FileName); //string for the filename of the image
                    string extension = Path.GetExtension(game.ImgFile.FileName); //string for the file extension
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddssff") + extension; //add the year month day second and milliseconds to the file name

                    //Add the filname to the model
                    game.ImgName = fileName;

                    //The output path where the image is stored
                    string path = Path.Combine(wwwRootPath + "/img/" + fileName);

                    // Move to folder
                    using(var filestream = new FileStream(path, FileMode.Create))
                    {
                        await game.ImgFile.CopyToAsync(filestream);
                    }

                    //Resize the images to thumbnails
                    CreateImageFiles(fileName);
                }
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConditionFK"] = new SelectList(_context.Conditions, "Id", "Condtition", game.ConditionFK);
            ViewData["GameConsoleFK"] = new SelectList(_context.GameConsoles, "Id", "ConsoleName", game.GameConsoleFK);
            ViewData["GameContentFK"] = new SelectList(_context.GameContents, "Id", "Content", game.GameContentFK);
            ViewData["PublisherFK"] = new SelectList(_context.Publishers, "Id", "PublisherName", game.PublisherFK);
            return View(game);
        }

        // Rezise images ------------------------------
        [Authorize]
        private void CreateImageFiles(string fileName)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            //create thumbnail
            using (var img = Image.FromFile(Path.Combine(wwwRootPath + "/img", fileName)))
            {
                img.Scale(60, 60).SaveAs(Path.Combine(wwwRootPath + "/img/thumb_" + fileName));
            }
        }

        // GET: Games/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["ConditionFK"] = new SelectList(_context.Conditions, "Id", "Condtition", game.ConditionFK);
            ViewData["GameConsoleFK"] = new SelectList(_context.GameConsoles, "Id", "ConsoleName", game.GameConsoleFK);
            ViewData["GameContentFK"] = new SelectList(_context.GameContents, "Id", "Content", game.GameContentFK);
            ViewData["PublisherFK"] = new SelectList(_context.Publishers, "Id", "PublisherName", game.PublisherFK);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImgFile,PublisherFK,GameConsoleFK,ConditionFK,GameContentFK")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }
            //checks if the image is uploaded
            if (game.ImgFile != null)
            {
                //uplode the image
                string wwwRootPath = _webHostEnvironment.WebRootPath; //the path to the wwwroot folder where the images gets saved
                string fileName = Path.GetFileNameWithoutExtension(game.ImgFile.FileName); //string for the filename of the image
                string extension = Path.GetExtension(game.ImgFile.FileName); //string for the file extension
                fileName = fileName + DateTime.Now.ToString("yyyyMMddssff") + extension; //add the year month day second and milliseconds to the file name

                //Add the filname to the model
                game.ImgName = fileName;

                //The output path where the image is stored
                string path = Path.Combine(wwwRootPath + "/img/" + fileName);

                // Move to folder
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await game.ImgFile.CopyToAsync(filestream);
                }

                //Resize the images to thumbnails
                CreateImageFiles(fileName);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["ConditionFK"] = new SelectList(_context.Conditions, "Id", "Condtition", game.ConditionFK);
            ViewData["GameConsoleFK"] = new SelectList(_context.GameConsoles, "Id", "ConsoleName", game.GameConsoleFK);
            ViewData["GameContentFK"] = new SelectList(_context.GameContents, "Id", "Content", game.GameContentFK);
            ViewData["PublisherFK"] = new SelectList(_context.Publishers, "Id", "PublisherName", game.PublisherFK);
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Condition)
                .Include(g => g.GameConsole)
                .Include(g => g.GameContent)
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
