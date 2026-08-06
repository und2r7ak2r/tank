using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShootingEditor2D
{
    public interface IStatSystem : ISystem
    {
        BindableProperty<int> KillCount { get; }
    }

    public class StatSystem : AbstractSystem, IStatSystem
    {
        protected override void OnInit()
        {
            
        }
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
    }
}