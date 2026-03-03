# GDIM32-Final
## Check-In
### Group Devlog
Our project uses raycasting to implement the item interaction system. The main feature that requires this is our pickup mechanic, where the player can press E to collect an object they are looking at. In our TryPickup() method, we create a Ray using new Ray(playerCamera.position, playerCamera.forward). This uses the camera’s forward direction vector to project a line into the world, and we call Physics.Raycast(ray, out hit, interactRange) to detect objects in that direction within a certain distance. The math behind this works because the ray uses a normalized direction vector to calculate positions along a line in 3D space like shown in the demo in class. If the ray intersects with a collider tagged "item", we store the object in the heldItem variable and attach it to the holdPoint. Raycasting is relevant in our project because it allows us to detect what the player is looking at using direction vectors instead of relying on trigger zones which makes the interaction system more precise and scalable. This use of vector direction and spatial detection is important to how our first-person interaction system works/functions.


### Gael Porras Devlog
At this stage of the project, I contributed by building the core first-person controller and interaction system in Unity. I wrote the PlayerController script, which includes variables such as moveSpeed, mouseSensitivity, interactRange, heldItem, and holdPoint, and methods like HandleMouseLook(), HandleMovement(), TryPickup(), DropItem(), and CheckForInteractable(). I added Rigidbody-based movement using Input.GetAxisRaw() for instant stopping, froze all Rigidbody rotations to prevent physics spinning, and separated horizontal and vertical camera rotation to create a stable FPS setup. I also built the item pickup system using a raycast from the camera that detects objects tagged “item,” parents them to the HoldPoint, disables their collider, and sets isKinematic to true, along with a drop system that reverses those changes. Additionally, I added a TextMeshPro “[E]” UI prompt that appears when looking at an interactable object. For the scene, I helped construct the house using multiple 3D cubes, set up grass material tiling to avoid stretching, used free assets to fill the environment with trees and large rocks, and configured 2 different instances of lighting. Our proposal was helpful conceptually, especially for system architecture ideas like the Singleton and FSM, however we underestimated the amount of foundational work needed for movement, physics handling, and interaction systems. Going forward, we would break systems into smaller technical steps earlier so details are clearer before the actual development begins.
### Jeremiah Yang Devlog
Put your individual final Devlog here.


## Final Submission
### Group Devlog
Put your group Devlog here.


### Team Member Name 1
Put your individual final Devlog here.
### Team Member Name 2
Put your individual final Devlog here.


## Open-Source Assets
Cite any open-source assets here. Put them in a LIST, and use correctly formatted LINKS.

- [Old-Cartoon Background Music](https://assetstore.unity.com/packages/audio/music/orchestral/old-cartoon-music-pack-free-277325)
- [Grass Material](https://assetstore.unity.com/packages/2d/textures-materials/glass/stylized-grass-texture-153153)
- [Trees](https://assetstore.unity.com/search#q=trees&nf-ec_price_filter=0...0)
- [Rocks and Bushes](https://assetstore.unity.com/packages/3d/environments/landscapes/low-poly-nature-pack-lite-40444)
- [Sun Model](https://assetstore.unity.com/packages/3d/environments/sci-fi/cosmokit-stylized-low-poly-planets-142199)
- [Really Cool Furtnite Pack!](https://assetstore.unity.com/packages/3d/props/interior/ultimate-interior-furniture-pack-low-poly-household-kitchen-prop-316897)
