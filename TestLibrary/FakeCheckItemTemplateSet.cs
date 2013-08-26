using ChecklistManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

namespace TestLibrary
{
    public class FakeCheckItemTemplateSet : FakeDbSet<CheckItemTemplate>
    {
        public override CheckItemTemplate Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Id == (int)keyValues.Single());
        }
    }
}
