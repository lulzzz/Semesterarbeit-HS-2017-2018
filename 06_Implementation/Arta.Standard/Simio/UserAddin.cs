using SimioAPI;
using SimioAPI.Extensions;
using System;
using System.Drawing;

namespace Simio
{
    public class UserAddin : IDesignAddIn
    {
        /// <summary>
        /// Property returning the name of the add-in. This name may contain any characters and is used as the display name for the add-in in the UI.
        /// </summary>
        public string Name
        {
            get { return "ArtaSource"; }
        }

        /// <summary>
        /// Property returning a short description of what the add-in does.
        /// </summary>
        public string Description
        {
            get { return "Generates a ArtaSource"; }
        }

        /// <summary>
        /// Property returning an icon to display for the add-in in the UI.
        /// </summary>
        public System.Drawing.Image Icon
        {
            get { return null; }
        }

        /// <summary>
        /// Method called when the add-in is run.
        /// </summary>
        public void Execute(IDesignContext context)
        {
            // This example code places some new objects from the Standard Library into the active model of the project.
            if (context.ActiveModel != null)
            {
                // Example of how to place some new fixed objects into the active model.
                // This example code places three new fixed objects: a Source, a Server, and a Sink.
                var intelligentObjects = context.ActiveModel.Facility.IntelligentObjects;
                var sourceObject = intelligentObjects.CreateObject("Source", new FacilityLocation(0, 0, 0)) as IFixedObject;
                
                context.ActiveModel.Elements.CreateElement("Arta");
       

            }
        }
    }
}
