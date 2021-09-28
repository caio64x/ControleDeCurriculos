﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MontagemCurriculo.Models;

namespace MontagemCurriculo.Controllers
{
    public class ObjetivosController : Controller
    {
        private readonly Contexto _context;

        public ObjetivosController(Contexto context)
        {
            _context = context;
        }

            

        // GET: Objetivos/Create
        public IActionResult Create(int  id)
        {
            Objetivo objetivo = new Objetivo();
            objetivo.CurriculoID = id;


            return View(objetivo);
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetivoID,Descrição,CurriculoID")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objetivo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","Curriculos", new { id = objetivo.CurriculoID });
            }
            return View(objetivo);
        }

        // GET: Objetivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objetivo = await _context.Objetivos.FindAsync(id);
            if (objetivo == null)
            {
                return NotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetivoID,Descrição,CurriculoID")] Objetivo objetivo)
        {
            if (id != objetivo.ObjetivoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objetivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjetivoExists(objetivo.ObjetivoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculos", new { id = objetivo.CurriculoID });
            }
            return View(objetivo);
        }

        // POST: Objetivos/Delete/5
        [HttpPost]
        
        public async Task<JsonResult> Delete(int id)
        {
            var objetivo = await _context.Objetivos.FindAsync(id);
            _context.Objetivos.Remove(objetivo);
            await _context.SaveChangesAsync();
            return Json("Objetivo excluído");
        }

        private bool ObjetivoExists(int id)
        {
            return _context.Objetivos.Any(e => e.ObjetivoID == id);
        }
    }
}
