# SimpleFpsControls

A much simplified version of the FPS controls, based on the FPS walker wiki script.

This include two scripts: **FPSWalker** to move around (+ jumping, falling, running, etc), and **FPSMouseLooking** to rotate camera based on mouse.

The right way to use them is like this:

```

[Scene]
    |
    [GameObject] Player
        |
        +-- [Component] Character Controller
        +-- [Component] FPSWalker
        +-- [GameObject] Player Looking
                |
                +-- [Component] FPSMouseLooking (CharacterControllerObj=Player)
                +-- [GameObject] Camera Container
                        |
                        +-- [Component] Camera

```

# FPSRigidWalker

Use this instead of the default FPSWalker if you want to use rigid bodies instead of a character controller.