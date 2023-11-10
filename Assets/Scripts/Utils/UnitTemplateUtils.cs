using System.Collections.Generic;

public static class UnitTemplateUtils
{
    public static UnitTemplate[] GetTemplatesOfType(this UnitTemplate[] _allTemplates, UnitType _unitType)
    {
        List<UnitTemplate> _filteredTemplates = new();

        foreach (UnitTemplate _template in _allTemplates)
        {
            if (_template.TypeOfUnit != _unitType)
            {
                continue;
            }
            
            _filteredTemplates.Add(_template);
        }

        return _filteredTemplates.ToArray();
    }
}
