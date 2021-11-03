# Unity Climbing Test Case Game
This is a game development test case about climbing a ragdoll and reach the target ledge. 

The mission is add jumping and climbing abilities to a ragdoll mechanim, so, ragdoll control is not easy especially if you want to **addForce to jump** to him. In this case I used a climbing library which is same with if I did. 

Dependencies kept minimum, I use a **climbing system** to add climb ability to ragdoll and **itween** for moving some obstacles (Actually there was no need)

## Demo and Game Play Video



You can download [demo apk](https://github.com/burakozturk16/unity-climbing-test-case/blob/main/Informations/DemoClimb.apk) file or watch game play video on [youtube](https://www.youtube.com/watch?v=216aCJqN6F0)

## Estimating and Diagrams

The estimated time to complete this prototype about 4-6 hours.

You can find the class structure and diagram [here](https://github.com/burakozturk16/unity-climbing-test-case/blob/main/Informations/diagram.pdf).

## Notes for Game Designer

I wanted to create a UI Toolkit window to createing levels automatically but this is only case :) so if you want to create new level there are some values to be tweak.

- You can create a new skin for hero with Assets->Create->Game->Player and this is a scriptableObject that has material to change player's skin.
- You need a empty game object and attach to it Level component to managing game.
- You can tweak the Player->Third Person System->Character Abilities to jump and climbing values.
- You need to add cinemachine.
- Also you need some put ledges and obstacles to a wall or mountain.
  -  The Ledges must have BoxCollider and Ledge Component, and please set first ledge and target ledge to reach.
  -  Obstacles must have BoxCollider and Obstacle Component.

The folder structure is well organized, so you can find easly what you search in; 
- Materials
- Models
- Scripts
- Scriptables
- Scenes
- Prefabs