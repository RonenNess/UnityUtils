# RecalcTextureTile

Recalculate texture tiling so that it will spread / tile evenly, no matter the surface size.
For example if you want that a quad in the size of 5x5 will always contain the whole texture exatly once, you can use this script to set it. Later if you scale the surface to 10x10, it will guarantee the texture will now appear 2x2 times, and won't be just stretched.

Note: this duplicate the material per object. While using shared material, this can still cause performance hit if you have lots of objects. 