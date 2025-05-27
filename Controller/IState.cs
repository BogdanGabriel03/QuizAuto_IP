using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    internal interface IState
    {
        void Excecute(StateController stateController);
    }
}
