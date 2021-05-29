﻿#region Access
using UnityEngine;
# endregion
namespace IntroScene
{
    /// <summary>
    /// Class used to manages the animations in intro scene
    /// in <see cref="Environment.Scenes.INTRO_SCENE"/>
    /// </summary>
    public class IntroGun : MonoBehaviour
    {
        #region Variables
        public ParticleSystem part_shot;
        #endregion
        #region
        /// <summary>
        /// Shows the shot animation
        /// </summary>
        public void GunAnimShot() => part_shot.Play();
        /// <summary>
        /// Call <see cref="IntroManager"/> to advice and then starts the end animation
        /// </summary>
        public void CallToEnd() => IntroManager.UIAnimEnd();
        #endregion
    }
}