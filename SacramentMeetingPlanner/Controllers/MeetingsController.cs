using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sacrament_Meeting_Planner.Data;
using Sacrament_Meeting_Planner.Models;

namespace SacramentMeetingPlanner.Controllers
{
  public class MeetingsController : Controller
  {
    private readonly SacramentContext _context;

    public MeetingsController(SacramentContext context)
    {
      _context = context;
    }

    // GET: Meetings
    public async Task<IActionResult> Index()
    {
      return _context.Meetings != null ?
                  View(await _context.Meetings.ToListAsync()) :
                  Problem("Entity set 'SacramentContext.Meetings'  is null.");
    }

    // GET: Meetings/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _context.Meetings == null)
      {
        return NotFound();
      }

      var meeting = await _context.Meetings
          .FirstOrDefaultAsync(m => m.Id == id);
      if (meeting == null)
      {
        return NotFound();
      }

      return View(meeting);
    }

    // GET: Meetings/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Meetings/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,DateOfMeeting,Leader,OpeningHymn,SacramentHymn,ClosingHymn,IntermediateNumber,OpeningPrayer,ClosingPrayer,MeetingSpeakers")] Meeting meeting)
    {
      if (ModelState.IsValid)
      {
        _context.Add(meeting);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(meeting);
    }


    // GET: Meetings/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || _context.Meetings == null)
      {
        return NotFound();
      }

      var meeting = await _context.Meetings.FindAsync(id);
      if (meeting == null)
      {
        return NotFound();
      }
      return View(meeting);
    }

    // POST: Meetings/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfMeeting,Leader,OpeningHymn,SacramentHymn,ClosingHymn,IntermediateNumber,OpeningPrayer,ClosingPrayer,MeetingSpeakers")] Meeting meeting)
    {
      if (id != meeting.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(meeting);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!MeetingExists(meeting.Id))
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
      return View(meeting);
    }

    // GET: Meetings/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null || _context.Meetings == null)
      {
        return NotFound();
      }

      var meeting = await _context.Meetings
          .FirstOrDefaultAsync(m => m.Id == id);
      if (meeting == null)
      {
        return NotFound();
      }

      return View(meeting);
    }

    // POST: Meetings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (_context.Meetings == null)
      {
        return Problem("Entity set 'SacramentContext.Meetings'  is null.");
      }
      var meeting = await _context.Meetings.FindAsync(id);
      if (meeting != null)
      {
        _context.Meetings.Remove(meeting);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool MeetingExists(int id)
    {
      return (_context.Meetings?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
