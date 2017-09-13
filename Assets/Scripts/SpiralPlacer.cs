using UnityEngine;

public class SpiralPlacer : PipeObstacleGenerator {

    public PipeObstacle[] obstaclePrefabs;

    public override void GenerateObstacles(Pipe pipe)
    {
        var start = (Random.Range(0, pipe.CurveSegmentCount) + 0.5f);
        var direction = Random.value < 0.5f ? 1f : -1f;

        var angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        for(var i = 0; i < pipe.CurveSegmentCount; i++)
        {
            var obstacle = Instantiate<PipeObstacle>(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
            var pipeRotation = (start + i * direction) * 360f / pipe.CurveSegmentCount;
            obstacle.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}
