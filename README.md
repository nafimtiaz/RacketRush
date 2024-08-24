# RacketRush

![racket_vr_collage](https://github.com/user-attachments/assets/df658e20-2451-43ff-a329-61e3639ae101)

RacketRush is an immersive VR game set within a dynamic dome environment where players must hit highlighted targets with a virtual racket. The game challenges players' reflexes and precision as they navigate changing surroundings and varying ball trajectories.

## Features

- **Dynamic Dome Environment**
  - Random target triangles appear on the dome wall.
  - Changeable surrounding environment with different themes.
  
- **Gameplay Mechanics**
  - A ball is thrown at random intervals from various directions.
  - The player must hit the highlighted target triangle within the dome.
  - The game ends after a set period.

- **Realistic Racket Physics**
  - Racket dynamically creates a rigidbody at runtime.
  - The racket view transform is anchored to the right-hand controller and updates each frame.
  - The dynamic rigidbody follows the racket view transform during the FixedUpdate cycle.

- **Customizable Game Settings**
  - In-game menu allows players to:
    - Toggle background music.
    - Change the surrounding environment.
    - Set the player's name before starting the game.
  
- **Leaderboard**
  - Track and display high scores.
  - Leaderboard data is saved locally on the device.


### Prerequisites
- VR headset (Oculus Quest/Quest2/Quest3 or compatible device).
- Unity 3D (version 2022.3.4f1 or higher).
