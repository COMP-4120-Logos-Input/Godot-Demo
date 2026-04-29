# Godot Demo (Logos.Input)

This is a simple grid-based game demo built with Godot. The goal is to collect a powerup used to jump over the holes
and get to the end of the level.

## Dependencies

- Godot Engine 4.6.2 – .NET
- .NET 10.0 SDK

## Setup

- Clone the repository to a local directory and open the directory in Godot.
- Run the game in the editor or build it for your platform and run the executable.

## Controls

- W A S D to move
- Space to jump (once powerup is collected)

## Implementation

The demo uses the Logos.Input library to handle input events. Godot's _Input method is overridden to convert Godot
events to Logos.Input events. Input events are then processed through Logos.Input's event dispatcher system and handled
with Godot's _Process method.

The demo also showcases the use of Logos.Input's KeyboardMapper class to map certain keys to specific actions. Two
different mappings are used, also known as contexts:

- Both contexts bind WASD to move
- The _poweredContext binds Space to jump
- Only one context is active at a time

## Going Further

There are many possibilities with implementing more contexts and mapping keys to actions. A more thorough version
of this demo should include:

- More levels
- More powerups
- Enemies
- Sound effects
- An in-game UI that displays the current state of the player