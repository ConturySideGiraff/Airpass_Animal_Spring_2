using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

[RequireComponent(typeof(Image))]
public class UIAnimFlipBook : UIAnim
{
    
    [SerializeField] private FlipBookName _flipBookName;
    [SerializeField] private bool _ignoreReduce = false;
    [SerializeField] private bool _isLoop = true;
    [SerializeField] private float _duration;
    [SerializeField] private List<Sprite> _sprites;

    private Image _image;
    
    public RectTransform Rt { get; private set; }

    public float Duration => _duration;

    protected override void Awake()
    {
        base.Awake();

        Rt = GetComponent<RectTransform>();
    }

    protected override IEnumerator CoAnim()
    {
        if (ReferenceEquals(_image, null))
        {
            _image = GetComponent<Image>();
        }

        do
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                _image.sprite = _sprites[i];

                for (float t = 0.0f; t <= _duration / _sprites.Count; t += _ignoreReduce ? Time.deltaTime : TimerManager.Instance.Reduce)
                {
                    yield return null; 
                }
            }
            
        } while (_isLoop);
        
        _endAction?.Invoke();
    }

    #region Editor_Only
#if UNITY_EDITOR
    [MenuItem("__CSG__/Enum/FlipBookNameUpdate")]
    public static void EnumUpdate()
    {
        const string enumName = "FlipBookName";
        
        string pilPath = Path.Combine(Application.dataPath, "__Data", "Scripts", "PIL",$"{enumName}.cs");
        string dirPath = Path.Combine(Application.dataPath, "__Data", "Sprites", "Animation");

        StringBuilder stringBuilder = new StringBuilder().Append($"public enum {enumName} ").Append("{\n");
        
        foreach (string directory in Directory.GetDirectories(dirPath))
        {
            string directoryName = new DirectoryInfo(directory).Name;

            stringBuilder.Append("  ").Append(directoryName).Append(",").Append("\n");
        }

        stringBuilder.Append("}");

        if (File.Exists(pilPath))
        {
            File.Delete(pilPath);
        }
        
        File.WriteAllText(pilPath, stringBuilder.ToString());
        
        AssetDatabase.Refresh();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SpriteUpdate()
    {
        string parentPath = Path.Combine(Application.dataPath, "__Data", "Sprites", "Animation",$"{_flipBookName}");
        string assetPath = Path.Combine("Assets", "__Data", "Sprites", "Animation",$"{_flipBookName}");
        
        _sprites.Clear();
        
        foreach (string file in Directory.GetFiles(parentPath, "*.png"))
        {
            string fileName = Path.GetFileName(file);

            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(assetPath, fileName));
            
            _sprites.Add(sprite);
        }

        if (_sprites.Count <= 0)
        {
            return;
        }

        GetComponent<Image>().sprite = _sprites[0];
    }
#endif
    #endregion
}

#region Editor_Only
#if UNITY_EDITOR
[CustomEditor(typeof(UIAnimFlipBook)), CanEditMultipleObjects]
public class UIAnimFlipBookEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("SpriteUpdate"))
        {
            (target as UIAnimFlipBook)?.SpriteUpdate();
        }
    }
}
#endif
#endregion
