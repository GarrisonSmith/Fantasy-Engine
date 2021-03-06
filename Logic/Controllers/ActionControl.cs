using System.Collections.Generic;

namespace Fantasy.Logic.Controllers
{
    /// <summary>
    /// Object used to describe what keys cause what actions.
    /// </summary>
    public class ActionControl
    {
        /// <summary>
        /// Static list containing all instances of this object.
        /// </summary>
        public static List<ActionControl> ControlActions = new List<ActionControl>();
        
        /// <summary>
        /// The action this ActionControl describes.
        /// </summary>
        public Actions action;
        /// <summary>
        /// The input that controls this ActionControl.
        /// </summary>
        public Inputs input;
        /// <summary>
        /// The GameTime that this ActionControl began being held.
        /// </summary>
        public double heldStartTime;
        /// <summary>
        /// True if this ActionControl was just pressed, False if not.
        /// </summary>
        public bool justTriggered = false;
        /// <summary>
        /// True if this ActionControl has been held, False if it was just pressed.
        /// </summary>
        public bool held = false;
        /// <summary>
        /// Describes the different contorl contexts this ActionControl can be applied to.
        /// </summary>
        public ControlContexts[] activeContexts;
        /// <summary>
        /// Describes the different control context this ActionControl will be disabled in.
        /// </summary>
        public ControlContexts[] disableContexts;

        /// <summary>
        /// Creates a ActionConrol with the provided parameters.
        /// </summary>
        /// <param name="action">The action this ActionControl describes.</param>
        public ActionControl(Actions action)
        {
            this.action = action;
            activeContexts = new ControlContexts[0];
            disableContexts = new ControlContexts[0];
            ControlActions.Add(this);
        }
        /// <summary>
        /// Creates a ActionConrol with the provided parameters.
        /// </summary>
        /// <param name="action">The action this ActionControl describes.</param>
        /// <param name="key">The key this ActionControl describes.</param>
        public ActionControl(Actions action, Inputs input) : this(action)
        {
            this.input = input;
        }
    }
}
