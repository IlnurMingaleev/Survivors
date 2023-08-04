using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class NoiseGenerator 
{
    public static float[,] GenerateNoise(int width, int height, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2Int offset)
    {
        float[,] noise = new float[width, height];
        System.Random rand = new System.Random(seed);

        Vector2[] octavesOffset = new Vector2[octaves];

        for (int i = 0; i < octaves; i++) 
        {
            float xOffset = rand.Next(-100000, 100000) + offset.x * (width / scale);
            float yOffset = rand.Next(-100000, 100000) + offset.y * (height / scale);

            octavesOffset[i] = new Vector2(xOffset / width, yOffset / height);
        }

        if (scale < 0) scale = 0.0001f;

        float halfWidth = width / 2.0f;
        float halfHeight = height / 2.0f;

        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                float superpositionCompensation = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float xResult = (x - halfWidth) / scale * frequency + octavesOffset[i].x * frequency;
                    float yResult = (y - halfHeight) / scale * frequency + octavesOffset[i].y * frequency;

                    float generateValue = Mathf.PerlinNoise(xResult, yResult);

                    noiseHeight += generateValue * amplitude;
                    noiseHeight -= superpositionCompensation;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                    superpositionCompensation = amplitude / 2;
                }

                noise[x, y] = Mathf.Clamp01(noiseHeight);
            } 
            
        }
        return noise;
    }
}
public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private SpriteTileMode _tile;
    [SerializeField] private Tilemap[] _tilemap;
    [SerializeField] private TileBase _grassTileBase;
    private int idxTilemap = 0;

    [Header("Camera")]
    [SerializeField] private Camera _camera;
    [Range(0, 50)] [SerializeField] private float _panSpeed = 5.0f;
    private Vector3 _panMovement;
    private Vector2Int _posCamera;
    private Vector3 _direction;

    [Header("Noise Configuration")]
    [SerializeField] private int _seed = 100;
    [Range(1, 100)] [SerializeField] private float _scale = 30.0f;
    [Range(1, 5)] [SerializeField] private int _octaves = 1;
    [Range(0, 1)] [SerializeField] private float _persistance = 0.5f;
    [SerializeField] private float _lacunaruty = 1.0f;

    [Serializable]

    private struct LevelTile 
    {
        public float _height;
        public TileBase _tileBase;
    }

    [Header("Terrain tile configuration")]
    [SerializeField] private List<LevelTile> _levelTiles = new List<LevelTile>();

    private int _noiseDimensionX = 150;
    private int _noiseDimensionY = 100;
    private float[,] noiseMap;

    private void TileMapInit() 
    {
        noiseMap = new float[_noiseDimensionX, _noiseDimensionY];

        _posCamera = new Vector2Int((int)_camera.transform.position.x, (int)_camera.transform.position.y);

        noiseMap = NoiseGenerator.GenerateNoise(_noiseDimensionX, _noiseDimensionY, _seed, 
            _scale, _octaves, _persistance, _lacunaruty, _posCamera);

        for (int yCoord = -(_noiseDimensionY / 2) ; yCoord < (_noiseDimensionY / 2); yCoord++)
        {
            for (int xCoord = -(_noiseDimensionX / 2); xCoord < (_noiseDimensionX / 2); xCoord++) 
            {
                _tilemap[1].SetTile(new Vector3Int(xCoord, yCoord, 0), _grassTileBase);
                _tilemap[0].SetTileFlags(new Vector3Int(xCoord, yCoord, 0), TileFlags.None);
                _tilemap[1].SetTileFlags(new Vector3Int(xCoord, yCoord, 0), TileFlags.None);
                foreach (var levelTile in _levelTiles) 
                {
                    if(noiseMap[xCoord + (_noiseDimensionX / 2), yCoord + (_noiseDimensionY / 2)] < levelTile._height)
                    {
                        _tilemap[0].SetTile(new Vector3Int(xCoord, yCoord, 0), levelTile._tileBase);
                        break;
                    }
                }
                
            }
        
        }
        _tilemap[0].gameObject.SetActive(true);
    }

    private void Awake()
    {
        if (_levelTiles.Count != 0)
        {
            _direction = Vector3.up;
        }
        else 
        {
            Debug.Log("Add level Tiles");
        }

        TileMapInit();

        StartCoroutine(GenerateTilemap());
    }
    private void Update()
    {
        _panMovement = new Vector3(0, 0, -10);
        if (Input.GetKey(KeyCode.S)) _direction = Vector3.down;
        if (Input.GetKey(KeyCode.W)) _direction = Vector3.up;
        if (Input.GetKey(KeyCode.A)) _direction = Vector3.left;
        if (Input.GetKey(KeyCode.D)) _direction = Vector3.right;

        _panMovement += _direction * _panSpeed * Time.deltaTime;

        _camera.transform.Translate(new Vector3(_panMovement.x, _panMovement.y, 0), Space.World);
        if (Mathf.Abs(_posCamera.x - _camera.transform.position.x) > _panSpeed - 1 ||
            Mathf.Abs(_posCamera.y - _camera.transform.position.y) > _panSpeed - 1)
        {
            StartCoroutine(GenerateTilemap());
        }

    }

    IEnumerator GenerateTilemap()
    {
        _posCamera = new Vector2Int((int)_camera.transform.position.x, (int)_camera.transform.position.y);
        noiseMap = NoiseGenerator.GenerateNoise(_noiseDimensionX, _noiseDimensionY, _seed, _scale, 
            _octaves, _persistance, _lacunaruty, _posCamera);

        idxTilemap = idxTilemap == 0 ? 1 : 0;
        _tilemap[idxTilemap].gameObject.SetActive(false);

        for (int yCoord = -(_noiseDimensionY / 2); yCoord < (_noiseDimensionY / 2); yCoord++) 
        {
            for (int xCoord = -(_noiseDimensionX / 2); xCoord < (_noiseDimensionY / 2); xCoord++) 
            {
                _tilemap[idxTilemap].SetTile(new Vector3Int(xCoord, yCoord, 0), _levelTiles[_levelTiles.Count - 1]._tileBase);
                foreach (var levelTile in _levelTiles) 
                {
                    if (noiseMap[xCoord + (_noiseDimensionX / 2), yCoord + _noiseDimensionY / 2] < levelTile._height) 
                    {
                        _tilemap[idxTilemap].SetTile(new Vector3Int(xCoord, yCoord, 0), levelTile._tileBase);
                        break;
                        
                      
                    }
                }
            }
            _tilemap[idxTilemap].gameObject.transform.position = new Vector2(_camera.transform.position.x, _camera.transform.position.y);
            yield return null;

        }
        _tilemap[idxTilemap].gameObject.SetActive(true);
    }
}
