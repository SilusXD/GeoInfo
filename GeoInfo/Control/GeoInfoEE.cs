using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoInfo.Model
{
    public static class GeoInfoEE
    {
        private static GeoInfoEntities _context;
        public static GeoInfoEntities GetContext()
        {
            if (_context == null)
            {
                _context = new GeoInfoEntities();
            }
            return _context;
        }
    }
}
