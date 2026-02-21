using UnityEngine;

public class TowerPlacementFree : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Vector2 footprintSize = new Vector2(1f, 1f);
    [SerializeField] private float footprintAngle = 0f;

    [Header("Placement rules")]
    [SerializeField] private LayerMask buildZoneMask;      // BuildZone colliders (trigger ok)
    [SerializeField] private LayerMask blockingMask;       // Towers, Path, Obstacles, Enemies, etc.

    [Header("Preview (optional)")]
    [SerializeField] private GameObject previewPrefab;

    private GameObject preview;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        currentTowerPrefab = towerPrefab;
        MakePreviewFor(currentTowerPrefab);
    }
    private void MakePreviewFor(GameObject prefab)
    {
        if (preview != null) Destroy(preview);

        if (prefab == null) return;

        preview = Instantiate(prefab);
        // Optional: make preview semi-transparent + disable colliders
        foreach (var col in preview.GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        var srs = preview.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
    }
    private void Update()
    {
        if (cam == null || currentTowerPrefab == null) return;

        Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0f;

        bool canPlace = CanPlaceAt(world);

        // Preview
        if (preview != null)
        {
            preview.transform.position = world;

            // Optional: color feedback if preview has SpriteRenderer
            var sr = preview.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, canPlace ? 0.7f : 0.25f);
        }

        // Click to place
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            Instantiate(currentTowerPrefab, world, Quaternion.identity);
        }
    }
    private GameObject currentTowerPrefab;

    public void SetCurrentTower(GameObject tower)
    {
        currentTowerPrefab = tower;
        MakePreviewFor(currentTowerPrefab);
    }

    private bool CanPlaceAt(Vector2 pos)
    {
        // 1) Must be inside a build zone
        Collider2D zone = Physics2D.OverlapPoint(pos, buildZoneMask);
        if (zone == null) return false;

        // 2) Must not overlap blockers
        Collider2D hit = Physics2D.OverlapBox(pos, footprintSize, footprintAngle, blockingMask);
        if (hit != null) return false;

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        // visualize footprint at mouse only in play mode is harder, so just show size concept
        Gizmos.DrawWireCube(transform.position, footprintSize);
    }
    public bool TryPlaceAtScreen(Vector2 screenPos, GameObject prefab)
    {
        if (prefab == null) return false;

        if (cam == null) cam = Camera.main;
        if (cam == null) return false;

        Vector3 world = cam.ScreenToWorldPoint(screenPos);
        world.z = 0f;

        if (!CanPlaceAt(world)) return false;

        Instantiate(prefab, world, Quaternion.identity);
        return true;
    }
}