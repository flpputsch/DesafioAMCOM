using System;

namespace AMcom.Teste.Service.DTOs
{
    public class CoordenadasDTO
    {
        private decimal _latitude;
        private decimal _longitude;

        public decimal Latitude
        {
            get => _latitude;
            private set
            {
                if (LatitudeValida(value))
                {
                    _latitude = value;
                }
                else
                {
                    throw new ArgumentException("Ponto de coordenada latitude inválido!");
                }
            }
        }

        public decimal Longitude
        {
            get => _longitude;
            private set
            {
                if (LongitudeValida(value))
                {
                    _longitude = value;
                }
                else
                {
                    throw new ArgumentException("Ponto de coordenada longitude inválido!");
                }
            }
        }

        public CoordenadasDTO(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        private bool LatitudeValida(decimal latitude)
        {
            return latitude <= 90 || latitude >= -90;
        }

        private bool LongitudeValida(decimal longitude)
        {
            return longitude <= 180 || longitude >= -180;
        }
    }
}