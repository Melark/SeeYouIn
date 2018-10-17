using SeeYouIn.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace SeeYouIn.ValidationRules
{
  public class ValidatableObject<T> : BaseViewModel, IValidity
  {
    private readonly List<IValidationRule<T>> _validations;
    private List<string> _errors;
    private T _value;
    private bool _isValid;


    private bool errorMessageVisible;
    public bool ErrorMessageVisible
    {
      get => errorMessageVisible;
      set
      {
        errorMessageVisible = value;
        OnPropertyChanged();
      }
    }

    public List<IValidationRule<T>> Validations => _validations;

    public List<string> Errors
    {
      get
      {
        return _errors;
      }
      set
      {
        _errors = value;
        OnPropertyChanged();
      }
    }

    public T Value
    {
      get
      {
        return _value;
      }
      set
      {
        _value = value;
        OnPropertyChanged();
      }
    }

    public bool IsValid
    {
      get
      {
        return _isValid;
      }
      set
      {
        _isValid = value;
        ErrorMessageVisible = !value;
        OnPropertyChanged();
      }
    }

    public ValidatableObject()
    {
      _isValid = true;
      errorMessageVisible = false;
      _errors = new List<string>();
      _validations = new List<IValidationRule<T>>();
    }

    public bool Validate()
    {
      Errors.Clear();

      IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
          .Select(v => v.ValidationMessage);

      Errors = errors.ToList();
      IsValid = !Errors.Any();

      return this.IsValid;
    }
  }
}
