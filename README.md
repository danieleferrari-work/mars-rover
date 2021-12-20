# Mars Rover
Knowledge test for CodicePlastico application

You’re part of the team that explores Mars by sending remotely controlled vehicles to the surface of the planet. Develop an API that translates the commands sent from earth to instructions that are understood by the rover.

Requirements:
<ul>
    <li>You are given the initial starting point (x,y) of a rover and the direction (N,S,E,W) it is facing.</il>
    <li>The rover receives a character array of commands.</li>
    <li>Implement commands that move the rover forward/backward (f,b).</li>
    <li>Implement commands that turn the rover left/right (l,r).</li>
    <li>Implement wrapping from one edge of the grid to another. (planets are spheres after all)</li>
    <li>Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point, aborts the sequence and reports the obstacle.</li>
</ul>
