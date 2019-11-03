using System.Collections.Generic;
using System.Linq;
using Zek.Extensions.Collections;

namespace Zek.Model.ViewModels
{
    public class DictionaryBaseViewModel : ReadOnlyViewModel
    {
        //todo: public override bool ReadOnly

        public DictionaryBaseViewModel(Dictionary<byte, string> cultures)
        {
            Texts.AddRange(cultures.Select(x => new TranslateTextViewModel { CultureId = x.Key, CultureName = x.Value }));
        }

        public DictionaryBaseViewModel()
        {
            Texts = new List<TranslateTextViewModel>();
        }

        protected void InitTextCulture(Dictionary<byte, string> cultures)
        {
            var tmpCultures = new Dictionary<byte, string>(cultures);

            if (Texts == null)
                Texts = new List<TranslateTextViewModel>();
            else
                foreach (var txt in Texts)
                {
                    if (string.IsNullOrEmpty(txt.CultureName))
                        txt.CultureName = cultures.TryGetValue(txt.CultureId);
                    tmpCultures.Remove(txt.CultureId);
                }

            foreach (var culture in tmpCultures)
            {
                Texts.Add(new TranslateTextViewModel { CultureId = culture.Key, CultureName = culture.Value });
            }

        }

        public List<TranslateTextViewModel> Texts { get; set; }


        public override bool ReadOnly
        {
            get { return false; }
        }
        //public void Init()
        //{
        //    foreach (var culture in Uow.DD_Cultures.GetAll())
        //    {
        //        Translates.Add(new TranslateTextViewModel
        //        {
        //            CultureID = culture.ID,
        //            CultureName = culture.Name,
        //            Text = string.Empty,
        //        });
        //    }
        //}

    }
}
