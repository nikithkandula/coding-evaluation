using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            //your code here
            var position = FindPositionByTitle(root, title);
            if(position != null)
            {
                var employee = new Employee(0, person);
                position.SetEmployee(employee);
            }
            return position;
        }

        private Position? FindPositionByTitle(Position position, string title)
        {
            Position? positionByTitle = null;
            if(position.GetTitle().Equals(title, StringComparison.InvariantCultureIgnoreCase))
            {
                positionByTitle = position;
                return positionByTitle;
            }

            var directReports = position.GetDirectReports();

            foreach (var p in directReports)
            {
                if (p.GetTitle().Equals(title, StringComparison.InvariantCultureIgnoreCase))
                {
                    positionByTitle = p;
                    break;
                }
                positionByTitle = FindPositionByTitle(p, title);
                if(positionByTitle != null)
                {
                    break;
                }

            }
            return positionByTitle;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
