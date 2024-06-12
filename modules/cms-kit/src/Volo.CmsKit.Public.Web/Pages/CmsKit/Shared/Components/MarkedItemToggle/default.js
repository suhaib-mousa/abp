(function ($) {

    abp.widgets.CmsMarkedItemToggle = function ($widget) {
        let widgetManager = $widget.data('abp-widget-manager');
        let $markedItemArea = $widget.find('.cms-markedItem-area');

        function getFilters() {
            return {
                entityType: $markedItemArea.attr('data-entity-type'),
                entityId: $markedItemArea.attr('data-entity-id')
            };
        }

        function setIconColor($icon) {
            var iconColor = $icon.css('color');
            $icon.css('-webkit-text-stroke-color', iconColor);
        }

        function isDoubleClicked(element) {
            if (element.data("isclicked")) return true;

            element.data("isclicked", true);
            setTimeout(function () {
                element.removeData("isclicked");
            }, 500);
        }

        function handleUnauthenticated() {
            // TODO: Handle the unauthenticated case
        }

        function registerClickOfMarkedItemIcon($container) {
            var $icon = $container.find('.cms-markedItem-icon');
            console.log($icon)

            if (isDoubleClicked($icon)) return;
            
            if ($icon.attr('data-is-authenticated') === 'false') {
                handleUnauthenticated();
                return;
            }
            $icon.click(function () {
                console.log('toggling...')
                volo.cmsKit.public.markedItems.markedItemPublic.toggle(
                    $markedItemArea.attr('data-entity-type'),
                    $markedItemArea.attr('data-entity-id')
                ).then(function () {
                    widgetManager.refresh($widget);
                });
            });
        }

        function init() {
            console.log('init')

            var $unmarked = $widget.find('.unmarked');
            if ($unmarked.length === 1) {
                setIconColor($unmarked);
            }
            registerClickOfMarkedItemIcon($widget);
        }

        return {
            init: init,
            getFilters: getFilters
        };
    };

})(jQuery);
