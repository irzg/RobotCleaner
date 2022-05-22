using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public enum RobotState
    {
        Idle,       // looking for crates
        DrivingToCrate,    // driving towards crate
        Grabbing,   // grabbing crate (ik)
        Attaching,  // attaching hand to crate
        Lifting,    // lifting the object
        Carrying,   // driving with crate attached
        Throwing,   // throwing crate
        Finished    // no more crates
    }
}
