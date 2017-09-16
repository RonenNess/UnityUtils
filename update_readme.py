import os


scripts_list = ""
for root, subdirs, files in os.walk("."):
    #if root.startswith(r".\_External"):
    #    continue
    print root
    for filename in files:
        if filename.endswith(".cs"):
            folder = os.path.join(root)[2:].replace("\\", "/")
            script_name = os.path.join(root, filename)[2:]
            scripts_list += "- " + ("[%s](%s)" % (script_name, folder)) + "\n"

with open("README.md", "w") as outfile:
    outfile.write("""
# Unity Scripts and Utils

Useful scripts & utils that I wrote / modified for Unity over the years.

## About the scripts

- All scripts are under the NesScript namespace, its recommended to replace it with your own game namespace to separate them from the standard assets.
- All scripts are under the MIT license and are free to use.
- To learn more about the scripts, check out the README.md file in each folder.

## Scripts

The following is a list with all scripts found in this repo:

__SCRIPTS_LIST__

For more info check out the README file in every script folder.

## _External

This repo is also used as a private collection of useful scripts I picked up along the road.

The **_External** folder contains scripts that I took as-is, as a whole, from other sources.
Nothing there is mine, not even edited.

## Missing Credits

Some of the scripts are based on free stuff I found over the years in forums etc. And some are based on older Unity projects I made, and the sources are unknown.

I try to give credit as much as I can (in the file headers) but its very much possible that I missed a spot, especially on older scripts.

So If you recognize your code but see no proper reference / credit, please let me know so I can fix it (just open a ticket on this git repo or send me an email).

Thanks!

""".replace("__SCRIPTS_LIST__", scripts_list))
