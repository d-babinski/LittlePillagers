


using UnityEngine;
using UnityEditor;

internal sealed class SpriteImportRule : AssetPostprocessor
{

    #region Methods

    //-------------Pre Processors

    // This event is raised when a texture asset is imported
    private void OnPreprocessTexture()
    {
        var fileNameIndex = assetPath.LastIndexOf('/');
        var fileName = assetPath.Substring(fileNameIndex + 1);

        if (!fileName.Contains(".psd") && !fileName.Contains(".png") && !fileName.Contains(".ase")) return;

        var importer = assetImporter as TextureImporter;

        importer.spritePixelsPerUnit = 32;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.spritePivot = new Vector2(0.5f, 0);
    }

    #endregion
}
