# TinyGo
**A modified version of the Chinese board game "Go"**

Built from the ground up, this project aims to autonomously play the board game using a Minimax tree AI system and Alpha-Beta pruning.

<p align="center"><a href = "https://www.youtube.com/watch?v=UIWq4CaDTHU" target="_blank"><img title="" src="https://github.com/stuartsoft/TinyGo/raw/main/sample.gif"/></a></p>

For simplicity, this game uses a modified scoring system in which the score is tabulated based on the number of pieces each player holds on the board. Grouped pieces can still be captured as in regular Go, and all stone liberties remain the same.

Because of the game's inherently high branching factor, the Minimax has been configured to run at a shallow depth of about 3-5 iterations on board sizes usually no larger than 7x7. This allows the application to perform at a reasonable rate, although with further optimizations and parallelized workloads, larger boards might be achievable.

**Hotkeys**

**ESC** = Open/Close game settings panel
