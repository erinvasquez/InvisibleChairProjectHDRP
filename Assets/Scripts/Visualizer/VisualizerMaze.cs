using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script assembled from https://catlikecoding.com/unity/tutorials/maze/
/// 
/// </summary>
public class VisualizerMaze : VisualizerUnit {
    VisualizerMazeManager mazeManager;
    
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 direction;

    public bool moving = false;
    public float speed = 0f;

    /// <summary>
    /// Set in Generate() when called by our VisualizerSpawner
    /// </summary>
    public IntVector2 mazeSize;

    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    public MazeDoor doorPrefab;

    [Range(0f, 1f)]
    public float doorProbability;

    /// <summary>
    /// An array of our maze cells
    /// </summary>
    private MazeCell[,] cells;
    List<MazeCell> UnfinishedCells;

    /// <summary>
    /// The amount of seconds to delay the next step in maze generation
    /// </summary>
    public float generationStepDelay = 0.01f;

    /// <summary>
    /// True if the maze is already done being generated
    /// </summary>
    public bool mazeGenerated = false;

    public int stepsTaken = 0;


    /// <summary>
    /// A property that returns a random IntVector2 coordinate
    /// </summary>
    public IntVector2 RandomCoordinates {

        get {
            return new IntVector2(Random.Range(0, mazeSize.x), Random.Range(0, mazeSize.z));
        }

    }

    /// <summary>
    /// Called by VisualizerMazeManager,
    /// 
    /// initializes our values
    /// </summary>
    /// <param name="manager">The VisualizerMazeManager that instantiated this object</param>
    /// <param name="startPos">Starting position vector for this maze</param>
    /// <param name="endPos">Ending position vector for this maze</param>
    /// <param name="dir">Direction vector for this maze</param>
    /// <param name="size">Maze size</param>
    /// <param name="genStepDelay">Time in seconds between generation steps</param>
    public void Initialize(VisualizerMazeManager manager, Vector3 startPos, Vector3 endPos, Vector3 dir, Vector3 rotation, IntVector2 size, float genStepDelay) {
        mazeManager = manager;
        startPosition = startPos;
        endPosition = endPos;
        direction = dir;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        mazeSize = size;
        generationStepDelay = (Conductor.secondsPerBeat * genStepDelay) / (mazeSize.x * mazeSize.z);

        transform.position = startPosition;
        //transform.parent = manager.transform; // already parented in VMManager

        speed = Vector3.Distance(startPosition, endPosition) / (float) (Conductor.secondsPerBeat * 16);

    }

    /// <summary>
    /// Reset our Maze to the start once it's reached the end
    /// </summary>
    private void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.visualizerReady) {
            return;
        }

        // If we've passed our endpoint,
        // Place our light at the back of the line again, and stop it from moving
        if (transform.position.z >= endPosition.z) {
            ResetMaze();
        }

        // If the light is supposed to be moving...
        if (moving) {
            // Move it forward enough for 1 update
            MoveMaze();
        }

        if (mazeGenerated) {
            // Re do our maze generation in VisualizerMazeManager
        }

    }

    public void MoveMaze() {
        speed = Vector3.Distance(startPosition, endPosition) / (float)(Conductor.secondsPerBeat * 16f);

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

    }

    public void ResetMaze() {
        moving = false;
        transform.position = startPosition;
    }



    /// <summary>
    /// Returns true if the coordinate can be contained by the maze
    /// </summary>
    /// <param name="coordinate">Coordinate for target MazeCell</param>
    /// <returns></returns>
    public bool ContainsCoordinates(IntVector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < mazeSize.x && coordinate.z >= 0 && coordinate.z < mazeSize.z;
    }

    /// <summary>
    /// Gets the cell at the IntVector2 coordinates provided
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public MazeCell GetCell(IntVector2 coordinates) {
        return cells[coordinates.x, coordinates.z];
    }

    /// <summary>
    /// Called by our VisualizerSpawner after Conductor
    /// signals the visualizer being ready.
    /// 
    /// A Coroutine that starts generating our maze by
    /// creating cells and the edges.
    /// 
    /// Sets MazeGenerated to true when finished
    /// </summary>
    /// <param name="mazeSize">Size of the maze</param>
    /// <returns></returns>
    public IEnumerator Generate(IntVector2 mazeSize) {
        this.mazeSize = mazeSize;

        // Suspend our coroutine for this long
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);

        // Make our MazeCell array
        cells = new MazeCell[this.mazeSize.x, this.mazeSize.z];

        // ActiveCells lists all maze cells that are "active"
        // Think Prim's algorithm. This is how we can manipulate the algorithm
        // Active meaning we've stepped on them and we're gonna
        // try backtracking to it
        UnfinishedCells = new List<MazeCell>();

        DoFirstGenerationStep(UnfinishedCells);

        while (UnfinishedCells.Count > 0) {
            yield return delay;

            DoNextGenerationStep(UnfinishedCells);
        }

        //Debug.Log("Maze Generation done in " + stepsTaken + " steps");
        mazeGenerated = true;

    }

    /// <summary>
    /// Called in Generate()
    /// </summary>
    /// <param name="activeCells"></param>
    private void DoFirstGenerationStep(List<MazeCell> activeCells) {
        activeCells.Add(CreateCell(RandomCoordinates));
        stepsTaken++;
    }

    /// <summary>
    /// "Retrieve the current cell, check whether the move is posible, and take care
    /// of removing cells from the [active cells] list"
    /// </summary>
    /// <param name="activeCells"></param>
    private void DoNextGenerationStep(List<MazeCell> activeCells) {
        stepsTaken++;
        // Set our current index as the last active cell
        int currentIndex = activeCells.Count - 1;
        
        // Get the last active cell
        MazeCell currentCell = activeCells[currentIndex];

        // If the cell is already ready, remove it from our active cells
        // and return
        if (currentCell.IsFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        // Get a direction for a random uninitialized edge
        MazeDirection direction = currentCell.RandomUninitializedDirection;

        // Set our coordinates to be the current one, plus one cell over (
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

        // If these coordinates are inside of our maze
        if (ContainsCoordinates(coordinates)) {
            // Get the neighbor that we're about to process at those coordinates
            MazeCell neighbor = GetCell(coordinates);

            // If there isn't a cell for that neighbor yet,
            if (neighbor == null) {
                // let's make it at those coordinates
                neighbor = CreateCell(coordinates);

                // Make a passage between these two cells
                CreatePassage(currentCell, neighbor, direction);
                
                // Add this neighbor to our active cells
                activeCells.Add(neighbor);
            } else {
                // There's already a cell for that neighbor, so just place a wall
                CreateWall(currentCell, neighbor, direction);
            }

        } else {
            // Coordinates for this cell aren't even in the maze, so just make a wall
            // This wall will be an outer edge for the maze.
            // THIS IS WHERE OUTSIDE WALLS ARE MADE
            CreateWall(currentCell, null, direction);
        }

    }

    /// <summary>
    /// Create a cell at the provided coordinates
    /// </summary>
    /// <param name="coordinates">An IntVector2 with X and Z position in our graph</param>
    /// <returns></returns>
    private MazeCell CreateCell(IntVector2 coordinates) {
        // Instantiate the cell's prefab as a MazeCell
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;

        // add this new cell to our cell array
        cells[coordinates.x, coordinates.z] = newCell;

        // Get the cell ready
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.Rotate(transform.localRotation.eulerAngles);
        newCell.transform.localPosition = new Vector3(coordinates.x - mazeSize.x * 0.5f + 0.5f, 0f, coordinates.z - mazeSize.z * 0.5f + 0.5f);

        return newCell;
    }

    /// <summary>
    /// Create a Maze Passage GameObject in between two cells, either
    /// a door or an "empty" passage
    /// </summary>
    /// <param name="cell">The current cell we're processing</param>
    /// <param name="otherCell">The next cell over</param>
    /// <param name="direction">The direction the next cell is in relation to the current cell</param>
    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        // Choose a prefab based on our probability of our passage being a door
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;

        // Instantiate and initialize our passage
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        
        // Instantiate our passagePrefab and initialize it in the Opposite direction
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    /// <summary>
    /// Create a Maze Wall GameObject in between our two cells
    /// Choose randomly from an array of Wall prefabs
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="otherCell"></param>
    /// <param name="direction"></param>
    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        // Choose a random wall prefab, instantiate, and initialize it
        MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);

        // If the other cell exists, instantiate and initialize again in the opposite direction
        if (otherCell != null) {
            wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }

    }


    

}
