using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CustomMvvm.Commands;

namespace CustomMvvm
{
    /// <summary>
    /// Wrapper for validation and edition of entities
    /// </summary>
    /// <typeparam name="T">Type of subject to represent</typeparam>
    public abstract class EntityViewModel<T> : ValidatableViewModel where T : class
    {
        protected readonly T Model;
        private bool _isToBeDeleted;
        private bool _alreadyNotifiedAboutChanges;

        /// <summary>
        /// Determines if view model is valid
        /// </summary>
        public bool IsValid => !HasErrors;

        /// <summary>
        /// Determines state of view model
        /// </summary>
        public bool HasChanges => HasBeenAdded || IsToBeDeleted || VerifyModelForChanges();
        
        /// <summary>
        /// Set true when entity doesn't exist yet in database
        /// </summary>
        public bool HasBeenAdded { get; set; }

        /// <summary>
        /// Set true when entity should be removed from database
        /// </summary>
        public bool IsToBeDeleted
        {
            get { return _isToBeDeleted; }
            set
            {
                if (_isToBeDeleted != value)
                {
                    _isToBeDeleted = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Commits changes to underlying model
        /// </summary>
        public ICommand SaveCommand { get; }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">Entity represented by this view model</param>
        protected EntityViewModel( T model )
        {
            if (model == null)
            {
                throw new ArgumentNullException( nameof( model ) );
            }
            Model = model;
            SaveCommand = new WatchingCommand<object>( o => SaveChangesToModel(), () => IsValid, this, nameof(HasErrors) );
        }

        /// <summary>
        /// Returns underlying entity.
        /// </summary>
        /// <returns>The entity</returns>
        public T GetModel()
        {
            return Model;
        }

        /// <summary>
        /// Apply properties modified in view model to underlying model.
        /// </summary>
        protected abstract void SaveChangesToModel();

        /// <summary>
        /// Iterate through exposed properties to determine if view model has been edited comparing to model.
        /// </summary>
        /// <returns>Returns true if properties doesn't match</returns>
        protected abstract bool VerifyModelForChanges();

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null )
        {
            if (!_alreadyNotifiedAboutChanges && propertyName != nameof(HasChanges))
            {
                _alreadyNotifiedAboutChanges = true;
                OnPropertyChanged( nameof(HasChanges) );
            }
            base.OnPropertyChanged( propertyName );
        }
    }
}
