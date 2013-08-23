         $('#star').raty({
             score: function () {
                 return $(this).attr('data-score');
             },
             path: '/Images/raty',
             number: 5,
             readOnly: true,
         });