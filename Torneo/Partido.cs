using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace linq.Torneo
{
    public class Partido
    {
        #region Properties  
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        #endregion Properties

        #region Initialize
        public Partido(Seleccion EquipoLocal, Seleccion EquipoVisitante) 
        {
            this.EquipoLocal = new Equipo(EquipoLocal);
            this.EquipoVisitante = new Equipo(EquipoVisitante);
        }
        #endregion Initialize
        #region Methods
        private void CalcularExpulsiones()
        {
            Random random = new Random();
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 50).ToList();
            List<String> JugadoresLocales = EquipoLocal.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            int position = random.Next(JugadoresLocales.Count);
            String expulsadoLocal = JugadoresLocales[position];
            position = random.Next(JugadoresVisitantes.Count);
            String expulsadoVisitante = JugadoresVisitantes[position];
            EquipoLocal.ExpulsarJugador(expulsadoLocal);
            EquipoVisitante.ExpulsarJugador(expulsadoVisitante);
        }

        private void CalcularResultado()
        {
            Random random = new Random();
            EquipoLocal.Goles = random.Next(0,6);
            EquipoVisitante.Goles = random.Next(0,6);
        }

        public void CalcularAmarillasLocal()
        {
            int t, position, i;
            String nombreAmarilla;
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 50).ToList();
            List<String> JugadoresLocales = EquipoLocal.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            Random random= new Random();
            t = random.Next(0,5);
            for( i = 0; i<t ; i++)
            {
                position = random.Next(JugadoresLocales.Count);
                nombreAmarilla = JugadoresLocales[position];
                EquipoLocal.amarillas(nombreAmarilla);
            }
        }
        public void CalcularAmarillasVisitante()
        {
            int t, position, i;
            String nombreAmarilla;
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 50).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            Random random= new Random();
            t = random.Next(0,5);
            for( i = 0; i<t ; i++)
            {
                position = random.Next(JugadoresVisitantes.Count);
                nombreAmarilla = JugadoresVisitantes[position];
                EquipoLocal.amarillas(nombreAmarilla);
            }
        }

        public string Resultado()
        {
            string resultado = "0 - 0";
            try
            {
                CalcularResultado();
                CalcularAmarillasLocal();
                CalcularAmarillasVisitante();
                CalcularExpulsiones();
                resultado = EquipoLocal.Goles.ToString() + " - " + EquipoVisitante.Goles.ToString();
            }
            catch(LoseForWException ex)
            {
                Console.WriteLine(ex.Message);
                EquipoLocal.Goles -= EquipoLocal.Goles;
                EquipoVisitante.Goles -= EquipoVisitante.Goles;
                if (ex.NombreEquipo == EquipoLocal.Seleccion.Nombre)
                {
                    EquipoVisitante.Goles += 3;
                    resultado = "0 - 3";
                }
                else
                {
                    EquipoLocal.Goles += 3;
                    resultado = "3 - 0";
                }
            }
            
            return resultado;
        }
        #endregion Methods

    }
}