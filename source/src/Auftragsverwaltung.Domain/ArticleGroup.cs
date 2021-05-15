﻿using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class ArticleGroup : EntityBase
    {
#nullable enable
        public int ArticleGroupId { get; set; }
        public string Name { get; set; }
        public int? ParentArticleGroupId { get; set; }
#nullable disable
    }
}
