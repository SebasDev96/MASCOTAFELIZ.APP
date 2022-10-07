using System;
using System.Collections.Generic;
using System.Linq;
using MascotaFeliz.App.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MascotaFeliz.App.Persistencia
{
    public class RepositorioMascota : IRepositorioMascota
    {
       
        private readonly AppContext _appContext;
        /// <summary>
        /// Metodo Constructor Utiiza 
        /// Inyeccion de dependencias para indicar el contexto a utilizar
        /// </summary>
        /// <param name="appContext"></param>//
        
        public RepositorioMascota(AppContext appContext)
        {
            _appContext = appContext;
        }

        public Mascota AddMascota(Mascota mascota)
        {
            var MascotaAdicionada = _appContext.Mascotas.Add(mascota);
            _appContext.SaveChanges();
            return MascotaAdicionada.Entity;
        }

        public void DeleteMascota(int idMascota)
        {
            var MascotaEncontrada = _appContext.Mascotas.FirstOrDefault(d => d.Id == idMascota);
            if (MascotaEncontrada == null)
                return;
            _appContext.Mascotas.Remove(MascotaEncontrada);
            _appContext.SaveChanges();
        }

        public IEnumerable<Mascota> GetMascotaPorVeterinario (int idVeterinario)
        {
            var mascotas = GetAllMascotas();
            List<Mascota> listMascotas = new List<Mascota>();
            if (mascotas != null)
            {
                if (idVeterinario > 0)
                {          
                    foreach (var item in mascotas)
                    {
                        if (item.Veterinario != null && item.Veterinario.Id == idVeterinario)
                       { 
                         listMascotas.Add(item);
                        } 
                         
                    }
                    
                 }
            
            }
        return listMascotas;
        }

        public IEnumerable<Mascota> GetMascotaPorDueno (int idDueno)
        {
            var mascotas = GetAllMascotas();
            List<Mascota> listMascotas = new List<Mascota>();
            if (mascotas != null)
            {
                if (idDueno > 0)
                {          
                    foreach (var item in mascotas)
                    {
                        if (item.Dueno != null && item.Dueno.Id == idDueno)
                       { 
                         listMascotas.Add(item);
                        } 
                         
                    }
                    
                 }
            
            }
        return listMascotas;
        }

        public IEnumerable<Mascota> GetMascotasPorFiltro(string filtro)
        {
            var mascotas = GetAllMascotas(); // Obtiene todos las mascotas
            if (mascotas != null)  //Si se tienen mascotas
            {
                if (!String.IsNullOrEmpty(filtro)) // Si el filtro tiene algun valor
                {
                    mascotas = mascotas.Where(s => s.Nombre.Contains(filtro));
                }
            }
            return mascotas;
        }

        public IEnumerable<Mascota> GetAllMascotas()
        {
            return _appContext.Mascotas.Include("Dueno").Include("Veterinario").Include("Historia");
        }

       public Mascota GetMascota(int idMascota)
        {
            return _appContext.Mascotas.Include("Dueno").Include("Veterinario").Include("Historia").FirstOrDefault(d => d.Id == idMascota);
        }

        public Mascota UpdateMascota(Mascota mascota)
        {
            var mascotaEncontrada = _appContext.Mascotas.FirstOrDefault(d => d.Id == mascota.Id);
            if (mascotaEncontrada != null)
            {
                mascotaEncontrada.Nombre = mascota.Nombre;
                mascotaEncontrada.Color = mascota.Color;
                mascotaEncontrada.Especie = mascota.Especie;
                mascotaEncontrada.Raza = mascota.Raza;
                mascotaEncontrada.Veterinario = mascota.Veterinario;
                mascotaEncontrada.Dueno = mascota.Dueno;
                
                _appContext.SaveChanges();
            }
            return mascotaEncontrada;
        }     
        public Veterinario AsignarVeterinario (int idMascota, int idVeterinario)
        {
            var mascotaEncontrada = _appContext.Mascotas.FirstOrDefault(m=>m.Id==idMascota);
            if(mascotaEncontrada!=null)
            {
                var veterinarioEncontrado =_appContext.Veterinarios.FirstOrDefault(v=>v.Id == idVeterinario);
                if (veterinarioEncontrado!=null)
                {
                    mascotaEncontrada.Veterinario= veterinarioEncontrado;
                    _appContext.SaveChanges();
                }
                return veterinarioEncontrado;
            }
            return null;
        }
        public Dueno AsignarDueno( int idMascota, int idDueno)
        {
            var mascotaEncontrada = _appContext.Mascotas.FirstOrDefault(m=>m.Id==idMascota);
            if(mascotaEncontrada!=null)
            {
                var duenoEncontrado =_appContext.Duenos.FirstOrDefault(d=>d.Id == idDueno);
                if (duenoEncontrado!=null)
                {
                    mascotaEncontrada.Dueno= duenoEncontrado;
                    _appContext.SaveChanges();
                }
                return duenoEncontrado;
            }
            return null;
        }
        public Historia AsignarHistoria (int idMascota, int idHistoria)
        {
            var mascotaEncontrada = _appContext.Mascotas.FirstOrDefault(m=>m.Id==idMascota);
            if(mascotaEncontrada!=null)
            {
                var historiaEncontrada =_appContext.Historias.FirstOrDefault(h=>h.Id == idHistoria);
                if (historiaEncontrada!=null)
                {
                    mascotaEncontrada.Historia= historiaEncontrada;
                    _appContext.SaveChanges();
                }
                return historiaEncontrada;
            }
            return null;
        }
    }
}