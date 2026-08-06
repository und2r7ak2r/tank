using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public abstract class ShootingEditor2DController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}