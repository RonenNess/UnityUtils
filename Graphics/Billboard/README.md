# Billboard

Billboards are quads that always face camera, used in 3D games.

The Billboard script in this folder implements the logic to make the quad always face camera, in 4 primary ways:

- Facing camera on all axis.
- Facing camera only on Y axis, based on camera direction.
- Facing camera only on Y axis, based on camera position.
- Facing camera only on Y axis, based on camera direction AND position.

Usually you would want to use the last option (camera direction + position) for all big sprites & billboards (trees, enemies, rocks, etc..), as it looks better and have less "weird" rotation when you stand close to it, compared to the other options.

For small sprites (grass, mushrooms, skull on the ground, etc.) the middle options are fine (they are faster but doesn't feel as right as the combined option).

And for effects (smoke, flare, etc..) you would want to use the first option (facing on all axis) since the effect should not be "standing" on the ground and probably looks better if totally facing the camera.