using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class TireliresController : Controller {
        IRepository<Produit> _repository;

        public TireliresController(IRepository<Produit> produitRepository) {
            _repository = produitRepository;
        }

        // GET: Tirelires
        public IActionResult Index() {
            //var tireliresContext = _context.Produit.Include(p => p.IdCategorieNavigation).Include(p => p.IdCouleurNavigation).Include(p => p.IdFabricantNavigation).Include(p => p.IdFournisseurNavigation);
            var liste = _repository.GetList();
            return View(liste);
        }

        // GET: Tirelires/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var produit = await _context.Produit
        //        .Include(p => p.IdCategorieNavigation)
        //        .Include(p => p.IdCouleurNavigation)
        //        .Include(p => p.IdFabricantNavigation)
        //        .Include(p => p.IdFournisseurNavigation)
        //        .FirstOrDefaultAsync(m => m.IdProduit == id);
        //    if (produit == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(produit);
        //}

        // GET: Tirelires/Create
        //public IActionResult Create()
        //{
        //    ViewData["IdCategorie"] = new SelectList(_context.Categorie, "IdCategorie", "Categorie1");
        //    ViewData["IdCouleur"] = new SelectList(_context.Couleur, "IdCouleur", "Couleur1");
        //    ViewData["IdFabricant"] = new SelectList(_context.Fabricant, "IdFabricant", "Nom");
        //    ViewData["IdFournisseur"] = new SelectList(_context.Fournisseur, "IdFournisseur", "Nom");
        //    return View();
        //}

        // POST: Tirelires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdProduit,Nom,PrixUnitaire,Hauteur,Longueur,Largeur,Poids,Capacite,Description,IdCouleur,IdFabricant,IdFournisseur,IdCategorie,Stock,Statut")] Produit produit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(produit);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdCategorie"] = new SelectList(_context.Categorie, "IdCategorie", "Categorie1", produit.IdCategorie);
        //    ViewData["IdCouleur"] = new SelectList(_context.Couleur, "IdCouleur", "Couleur1", produit.IdCouleur);
        //    ViewData["IdFabricant"] = new SelectList(_context.Fabricant, "IdFabricant", "Nom", produit.IdFabricant);
        //    ViewData["IdFournisseur"] = new SelectList(_context.Fournisseur, "IdFournisseur", "Nom", produit.IdFournisseur);
        //    return View(produit);
        //}

        //// GET: Tirelires/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var produit = await _context.Produit.FindAsync(id);
        //    if (produit == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdCategorie"] = new SelectList(_context.Categorie, "IdCategorie", "Categorie1", produit.IdCategorie);
        //    ViewData["IdCouleur"] = new SelectList(_context.Couleur, "IdCouleur", "Couleur1", produit.IdCouleur);
        //    ViewData["IdFabricant"] = new SelectList(_context.Fabricant, "IdFabricant", "Nom", produit.IdFabricant);
        //    ViewData["IdFournisseur"] = new SelectList(_context.Fournisseur, "IdFournisseur", "Nom", produit.IdFournisseur);
        //    return View(produit);
        //}

        //// POST: Tirelires/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdProduit,Nom,PrixUnitaire,Hauteur,Longueur,Largeur,Poids,Capacite,Description,IdCouleur,IdFabricant,IdFournisseur,IdCategorie,Stock,Statut")] Produit produit)
        //{
        //    if (id != produit.IdProduit)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(produit);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProduitExists(produit.IdProduit))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdCategorie"] = new SelectList(_context.Categorie, "IdCategorie", "Categorie1", produit.IdCategorie);
        //    ViewData["IdCouleur"] = new SelectList(_context.Couleur, "IdCouleur", "Couleur1", produit.IdCouleur);
        //    ViewData["IdFabricant"] = new SelectList(_context.Fabricant, "IdFabricant", "Nom", produit.IdFabricant);
        //    ViewData["IdFournisseur"] = new SelectList(_context.Fournisseur, "IdFournisseur", "Nom", produit.IdFournisseur);
        //    return View(produit);
        //}

        //// GET: Tirelires/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var produit = await _context.Produit
        //        .Include(p => p.IdCategorieNavigation)
        //        .Include(p => p.IdCouleurNavigation)
        //        .Include(p => p.IdFabricantNavigation)
        //        .Include(p => p.IdFournisseurNavigation)
        //        .FirstOrDefaultAsync(m => m.IdProduit == id);
        //    if (produit == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(produit);
        //}

        //// POST: Tirelires/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var produit = await _context.Produit.FindAsync(id);
        //    _context.Produit.Remove(produit);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProduitExists(int id)
        //{
        //    return _context.Produit.Any(e => e.IdProduit == id);
        //}
    }
}
