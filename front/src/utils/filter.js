import Piii from "piii";
import piiiFilters from "piii-filters";

const swearwordFilter = new Piii({
  filters: [...Object.values(piiiFilters)],
});

export default swearwordFilter;