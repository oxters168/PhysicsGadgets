# PhysicsGadgets
Interfaces or items that are interactible through physics.

# Installation
Before installing, you must have [UnityHelpers](https://github.com/oxters168/UnityHelpers) set up in your project. Then you can either clone this repository into your project, download the latest unity package from releases and import, or download as a zip through github (not recommended due to the [git lfs issue](https://github.com/git-lfs/git-lfs/issues/903)).

# How to Use
To use any of the items inside the package, find their prefab in the 'Prefabs' folder and drag them into the scene.

# What's Inside
1. Button:
   - Has an onDown event that can be subscribed to from script or editor
   - Only fires onDown event when when pressed down beyond an adjustable point
   
   ![Button](https://i.imgur.com/oSEZ7UP.gif)
1. Lever:
   - Can toggle between on and off
   - Has an onValueChanged event that can be subscribed to from script or editor
   
   ![Lever](https://i.imgur.com/SaZ6YDM.gif)
1. Joystick:
   - Has two axes of input
   - Returns to place when not being pushed
   
   ![Joystick](https://i.imgur.com/yK0kVSi.gif)
1. Switch:
   - Can toggle between on and off
   - Has an onValueChanged event that can be subscribed to from script or editor
   
   ![Switch](https://i.imgur.com/abtlu6M.gif)
