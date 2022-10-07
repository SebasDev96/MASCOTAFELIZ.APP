using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MascotaFeliz.App.Dominio;
using MascotaFeliz.App.Persistencia;

namespace MascotaFeliz.App.Frontend.Pages
{
    public class DetallesVeterinarioModel : PageModel
    {
        private static IRepositorioVeterinario _repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
        public Veterinario veterinario {get;set;}


        public IActionResult OnGet(int veterinarioId)
        {
            veterinario = _repoVeterinario.GetVeterinario(veterinarioId);
            if(veterinario == null)
            {
                return RedirectToPage("./NotFound");
            }
            else
            {
                return Page();
            }
         }
    }
}
