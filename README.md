# Triple-Affirmative-Narrative-System
A system to streamline the creation of complex branching dialogue trees and events in Unity.

## Usage Instructions

### 1\. Simple Linear Dialogue

To use the Narrative System to create a simple dialogue interaction, the following things are needed:

1. A single instance of the “Canvas” prefab must be in the scene   
2. At least one instance of the NPC prefab must be in the scene   
3. A player with the “PlayerMove” script must be in the scene. The header “Dialogue UI” in the script component must be set to the instance of the Canvas that is in the scene.

Once this is set up, it is easy to create a simple, linear dialogue interaction:

1. In the assets folder, in a subfolder of your choice, right-click and Create \-\> Dialogue \-\> DialogueObject. This will create a Dialogue Object, which should be given a distinctive name.  
2. In the “Dialogue” header of your dialogue object, create as many lines as needed, and fill them with your dialogue. These lines will be shown linearly, one after the other.  
3. Go to an NPC in your scene. In the headers for the “Dialogue Activator” script, add a “Dialogue Object.” Then, put the dialogue object you just created into this field. 

Now, once the player approaches the NPC in play mode and presses the interact key (Default: ‘E’), the dialogue you created is displayed.

### 2\. Branching Dialogue With Fixed Responses 

In order to create branching dialogue, the player must create one or more responses in the “Responses” field inside a dialogue object. Each Response has 3 fields: response text, dialogue object, and conditions. Conditions are optional and will be ignored for now.   
The “response text” field is the text that the player will have the option to select. The “dialogue object” field is the dialogue that will be triggered.   


![alt text](https://github.com/Salvale/Triple-Affirmative-Narrative-System/tree/main/images/tree-structure.png "tree image")
Further branching responses can be nested into the dialogue objects that are associated with a response, and this can be repeated ad infinitum. 

**For all of the following uses, a “scene manager” singleton object is needed. This object must be tagged “GameController,” and have the “Var Tracker” script attached to it.** 

- The system expects that only one object in the scene is tagged as GameController. Having more than one object with this tag may cause unexpected behaviours. 

### 3\. Branching Dialogue with Variable Responses

To make it so that certain dialogue responses only show when a certain condition is met, add one or more item to the “Conditions” field in a response. A condition object has 3 components: Key, Comparator, and Value. In order for a conditional to be valid, a few things have to be true:

1. The “Key” string must exist as a key within the “variables” dictionary inside the “VarTracker” script.  
2. The Comparator must be one of the following: \>, \>=, \<, \<=, \=, \==, or \!=.  
3. The “Value” field must be an integer

If these three things are done right, the conditional will be properly evaluated without an error. In order for the response to show, the conditional must evaluate to true. Otherwise, the response will not appear.

### 4\.  Different Starting Points in Dialogue

In order to have an NPC have different starting points in dialogue, you must add multiple dialogue objects to the NPC’s Dialogue Activator script. If there are *n* dialogue starts, there must be exactly *n \- 1* conditions. The lowest dialogue object will be the “default” dialogue object.   
These dialogue objects will be evaluated one by one, from top to bottom, against their conditionals (i.e., the first dialogue object is associated with the first conditional, etc.).   

![alt text](https://github.com/Salvale/Triple-Affirmative-Narrative-System/tree/main/images/twoDialogueStarts.png "Top goes")
In this example, Element 0 will be associated with the condition TalkedCount \> 0\. Element 1 is not associated with any conditional.   
The first dialogue object with a conditional that evaluates to “True” is the one that will be displayed.

![alt text](https://github.com/Salvale/Triple-Affirmative-Narrative-System/tree/main/images/priorityExplanation.png "tree image2")
If none of the conditions evaluate to True, then the dialogue object in the lowest position will always be shown. For this reason, it is nonsensical to give the lowest dialogue object a conditional. 

### 5\. Tracking Player Responses & Interactions

Tracking player interactions is extremely simple. In any dialogue object, add one or more events to the “Trigger Event” field. The expected input for events consists of three things:

1. The “Key” string must exists as a key within the “variables” dictionary inside of the “VarTracker” script.  
2. A “modifier,” which can be one of the following:  
- “c” or “change”: This will add the “value” int to the vartracker variable that corresponds to the key.  
- “s” or “set”: This will set the item that corresponds to the varTracker variable to the key.  
3. A “value,” which is an integer that will be applied to the varTracker variable in one of two ways specified above.

Whenever the dialogueObject is triggered, all “Trigger Events” will occur and effect the variables stored in the varTracker.

### 6\. Loading Different Room Content Based on the varTracker

The Narrative System is capable of loading different “suites” of items when a scene is loaded, based on certain variables in the varTracker. To do this, take the game objects you want to conditionally load, and drag them into the “suites” field. For each suite you include, add a condition that will be associated with that suite.  

![alt text](https://github.com/Salvale/Triple-Affirmative-Narrative-System/tree/main/images/suites.png "suites")
In this example, SuiteA will be loaded when TalkedCount \> 0, and SuiteB will be loaded when Talked Count \== 0\.  
If the conditionals are not mutually exclusive, then all suites whose conditions are met will be loaded at once. If a suite does not have a conditional associated with it, it will never be loaded. This means that the list of conditions should always be the same length as the list of suites. 
